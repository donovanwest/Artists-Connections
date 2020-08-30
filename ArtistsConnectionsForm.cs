using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI.Web;
using Microsoft;
using Microsoft.Msagl.Drawing;
using System.Threading;
using Microsoft.Msagl.Core.DataStructures;
using Priority_Queue;

namespace ArtistsConnectionsFramework
{
    public partial class ArtistsConnectionsForm : Form
    {
        SpotifyClient Spotify;
        Graph ArtistsGraph = new Graph("Artists' Connections");
        SimplePriorityQueue<string> ArtistsToCheck = new SimplePriorityQueue<string>();
        List<string> CheckedArtists = new List<string>();
        Dictionary<string, string> ArtistIdToName = new Dictionary<string, string>();
        //HashSet<Edge> Edges = new HashSet<Edge>();

        public ArtistsConnectionsForm()
        {
            InitializeComponent();
            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator("e6bf2e305d98443190c472ee318fd511", "96bad35ecf9c41f581a761eb3a85348b"));
            Spotify = new SpotifyClient(config);
            ArtistsToCheck.Enqueue("4JxdBEK730fH7tgcyG3vuv", 0);
            int n = 0;
            while (ArtistsToCheck.Count > 0 && ArtistsToCheck.GetPriority(ArtistsToCheck.First) <= 2)
            {
                string first = ArtistsToCheck.First;
                float priority = ArtistsToCheck.GetPriority(first);
                ArtistsToCheck.Dequeue();                   
                Task<List<string>> artistsTask = FindAllConnectedArtists(first, priority);
                artistsTask.Wait();
                n++;                        
            }
            List<string> edges = new List<string>();
            foreach(Edge edge in ArtistsGraph.Edges)
            {
                string sourceName;
                string targetName;
                ArtistIdToName.TryGetValue(edge.Source, out sourceName);
                ArtistIdToName.TryGetValue(edge.Source, out targetName);
                edges.Add(sourceName + " - " + edge.LabelText + " - " + targetName);
            }
            ListBox1.DataSource = edges;
            while (ArtistsToCheck.Count > 0)
            {
                string name;
                ArtistIdToName.TryGetValue(ArtistsToCheck.First, out name);
                float priority = ArtistsToCheck.GetPriority(ArtistsToCheck.First);
                ArtistsToCheck.Dequeue();
                ListBox3.Items.Add(name + " - " + priority);
            }
            foreach (string id in CheckedArtists)
            {
                string name;
                ArtistIdToName.TryGetValue(id, out name);
                ListBox2.Items.Add(name);
            }
            label1.Text = ListBox1.Items.Count.ToString();
            label2.Text = ListBox2.Items.Count.ToString();
            label3.Text = ListBox3.Items.Count.ToString();
        }

        public Task<List<string>> FindAllConnectedArtists(string artistID, float priority)
        {
            return Task.Run(() =>
            {
                CheckedArtists.Add(artistID);
                List<string> artistNames = new List<string>();
                HashSet<string> connectedArtists = new HashSet<string>();
                Paging<SimpleAlbum> pagingAlbums;
                try
                {
                    Task<Paging<SimpleAlbum>> pagingAlbumsTask = Spotify.Artists.GetAlbums(artistID);
                    pagingAlbumsTask.Wait();
                    pagingAlbums = pagingAlbumsTask.Result;
                } catch {
                    Thread.Sleep(3500);
                    Task<Paging<SimpleAlbum>> pagingAlbumsTask = Spotify.Artists.GetAlbums(artistID);
                    pagingAlbumsTask.Wait();
                    pagingAlbums = pagingAlbumsTask.Result;
                }                       
                List<string> albumIDs = new List<string>();           
                foreach (SimpleAlbum simpleAlbum in pagingAlbums.Items)
                {
                    albumIDs.Add(simpleAlbum.Id);
                }
                if (albumIDs.Count > 0)
                {
                    AlbumsRequest request = new AlbumsRequest(albumIDs);
                    AlbumsResponse albumsResponse;
                    try
                    {
                        Task<AlbumsResponse> fullAlbumTask = Spotify.Albums.GetSeveral(request);
                        fullAlbumTask.Wait();
                        albumsResponse = fullAlbumTask.Result;
                    }
                    catch
                    {
                        Thread.Sleep(3500);
                        Task<AlbumsResponse> fullAlbumTask = Spotify.Albums.GetSeveral(request);
                        fullAlbumTask.Wait();
                        albumsResponse = fullAlbumTask.Result;
                    }

                    foreach (FullAlbum fullAlbum in albumsResponse.Albums)
                    {
                        Paging<SimpleTrack> simpleTracks = fullAlbum.Tracks;
                        foreach (SimpleTrack simpleTrack in simpleTracks.Items)
                        {
                            List<SimpleArtist> simpleArtistsList = simpleTrack.Artists;
                            List<String> simpleArtistsOnTrack = new List<string>();
                            foreach (SimpleArtist simpleArtist in simpleArtistsList)
                                simpleArtistsOnTrack.Add(simpleArtist.Id);
                            if (simpleArtistsOnTrack.Contains(artistID))
                            {
                                foreach (SimpleArtist simpleArtist in simpleTrack.Artists)
                                {
                                    if (!ArtistIdToName.ContainsKey(simpleArtist.Id))
                                        ArtistIdToName.Add(simpleArtist.Id, simpleArtist.Name);
                                    if (!ArtistsToCheck.Contains(simpleArtist.Id) && !CheckedArtists.Contains(simpleArtist.Id))
                                    {
                                        ArtistsToCheck.Enqueue(simpleArtist.Id, priority + 1);
                                        Edge edge = new Edge(artistID, simpleTrack.Name, simpleArtist.Id);
                                        if (!containsEdge(edge))
                                        {
                                            ArtistsGraph.AddEdge(artistID, simpleTrack.Name, simpleArtist.Id);
                                            //Edges.Add(edge);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return connectedArtists.ToList();
            });
        }

        public bool containsEdge(Edge edge)
        {
            foreach(Edge e in ArtistsGraph.Edges)
            {
                if (e.Source.Equals(edge.Source) && e.LabelText.Equals(edge.LabelText) && e.Target.Equals(edge.Target))
                    return true;
            }
            return false;
        }
    }
}
