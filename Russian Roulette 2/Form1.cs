using Grpc.Net.Client;
using Grpc.Core;
using Russian_Roulette.LayoutObjects;
using System.Net;

namespace Russian_Roulette{
    public partial class Form1 : Form{
        MainService.MainServiceClient sender;
        Server listener;
        public ListenerResponsivePanel currentPanel;
        uint gameID;
        uint inGameID;
        string publicIP;
        int listenerPort = 2137;

        public Form1(){
            InitializeComponent();
            get_public_ip();

            currentPanel = create_main_panel();
            //currentPanel = create_game_panel(new string[6] {"G1","G2","G3","G4","G5","G6" }, 0);
            Controls.Add(currentPanel);
            var channel = GrpcChannel.ForAddress("http://192.168.26.132:2048");
            sender = new MainService.MainServiceClient(channel);
            Thread listener_thread= new Thread(serve_messages);
            listener_thread.Start();

        }

        void serve_messages(){
            bool serverWorks = false;
             do{
                try{
                    listener = new Server{
                        Services = { ClientListener.BindService(new ClientListenerServer(this)) },
                        Ports = { new ServerPort("0.0.0.0", listenerPort, ServerCredentials.Insecure) }
                    };
                    listener.Start();
                    serverWorks = true;
                }
                catch (Exception e){
                    listenerPort++;
                }
            }while (!serverWorks);
        }

        void get_public_ip(){
            var host = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (var hostEntry in host)
            {
                if (hostEntry.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    continue;
                }
                var ip_string = hostEntry.ToString();
                var bytes = ip_string.Split('.');
                if (bytes[0] != "192" && bytes[1] != "168")
                {
                    publicIP = ip_string;
                    break;
                }
            }
        }

    }
}