
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RiseEngine.Core.Network
{
    public class HTTPServer
    {
        #region Global Variable


        public bool running = false;

        public string Server_name = "Maker-Rise-Http-Server ";

        public string errore_404 = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body><h2>WebGive - 1.3</h2><div>404 - Not Found</div></body></html>";

        public string errore_501 = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body><h2>WebGive - 1.3</h2><div>501 - Method Not Implemented</div></body></html>";

        public string main_doc = "index";

        public readonly string server_version = "1.3";

        public int timeout = 8;

        private Encoding charEncoder = Encoding.UTF8;

        private Socket serverSocket;

        private string contentPath;

        #endregion

        #region Constructor


        #endregion

        private Dictionary<string, string> extensions = new Dictionary<string, string>
        {
            {"htm", "text/html"},
            {"html","text/html"},
            {"xml","text/xml"},
            {"txt","text/plain"},
            {"css","text/css"},
            {"png","image/png"},
            {"gif","image/gif"},
            {"jpg","image/jpg"},
            {"jpeg","image/jpeg"},
            {"zip","application/zip"}
        };

        public bool start(IPAddress ipAddress, int port, int maxNOfCon, string contentPath)
        {
            bool result;

            if (this.running)
            {
                result = false;
            }
            else
            {
                //Preparing web server.
                try
                {
                    //seting up vars.
                    this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.serverSocket.Bind(new IPEndPoint(ipAddress, port));
                    this.serverSocket.Listen(maxNOfCon);
                    this.serverSocket.ReceiveTimeout = this.timeout;
                    this.serverSocket.SendTimeout = this.timeout;
                    this.running = true;
                    this.contentPath = contentPath;
                }
                catch
                {
                    //Andeling Error.
                    result = false;
                    return result;
                }

                //Setup Thread.
                ThreadStart MainThreatLoop = new ThreadStart(delegate
               {

                   while (this.running)
                   {
                       try
                       {
                           Socket clientSocket = this.serverSocket.Accept();


                           ThreadStart RequestHandle = new ThreadStart(delegate
                           {
                               clientSocket.ReceiveTimeout = this.timeout;
                               clientSocket.SendTimeout = this.timeout;
                               try
                               {
                                   this.handleTheRequest(clientSocket);
                               }
                               catch
                               {
                                   try
                                   {
                                       clientSocket.Close();
                                   }
                                   catch
                                   {
                                   }
                               }
                           });


                           Thread thread2 = new Thread(RequestHandle);
                           thread2.Start();
                       }
                       catch
                       {
                       }
                   }
               });


                Thread thread1 = new Thread(MainThreatLoop);

                //start the serveur
                thread1.Start();
                result = true;
            }
            return result;
        }

        public void stop()
        {
            if (this.running)
            {
                this.running = false;
                try
                {
                    this.serverSocket.Close();
                }
                catch
                {
                }
                this.serverSocket = null;
            }
        }

        private void handleTheRequest(Socket clientSocket)
        {
            byte[] array = new byte[10240];
            int count = clientSocket.Receive(array);
            string @string = this.charEncoder.GetString(array, 0, count);
            string text = @string.Substring(0, @string.IndexOf(" "));
            int num = @string.IndexOf(text) + text.Length + 1;
            int num2 = @string.LastIndexOf("HTTP") - num - 1;
            string text2 = @string.Substring(num, num2);
            if (text.Equals("GET") || text.Equals("POST"))
            {
                string text3 = text2.Split(new char[]
                {
                    '?'
                })[0];
                text3 = text3.Replace("/", "\\").Replace("\\..", "");
                num = text3.LastIndexOf('.') + 1;
                if (num > 0)
                {
                    num2 = text3.Length - num;
                    string key = text3.Substring(num, num2);
                    if (this.extensions.ContainsKey(key))
                    {
                        if (File.Exists(this.contentPath + text3))
                        {
                            this.sendOkResponse(clientSocket, File.ReadAllBytes(this.contentPath + text3), this.extensions[key]);
                        }
                        else
                        {
                            this.notFound(clientSocket);
                        }
                    }
                }
                else
                {
                    if (text3.Substring(num2 - 1, 1) != "\\")
                    {
                        text3 += "\\";
                    }
                    if (File.Exists(this.contentPath + text3 + this.main_doc + ".htm"))
                    {
                        this.sendOkResponse(clientSocket, File.ReadAllBytes(string.Concat(new string[]
                        {
                            this.contentPath,
                            text3,
                            "\\",
                            this.main_doc,
                            ".htm"
                        })), "text/html");
                    }
                    else if (File.Exists(this.contentPath + text3 + this.main_doc + ".html"))
                    {
                        this.sendOkResponse(clientSocket, File.ReadAllBytes(string.Concat(new string[]
                        {
                            this.contentPath,
                            text3,
                            "\\",
                            this.main_doc,
                            ".html"
                        })), "text/html");
                    }
                    else
                    {
                        this.notFound(clientSocket);
                    }
                }
            }
            else
            {
                this.notImplemented(clientSocket);
            }
        }

        private void notImplemented(Socket clientSocket)
        {
            this.sendResponse(clientSocket, this.errore_501, "501 Not Implemented", "text/html");
        }

        private void notFound(Socket clientSocket)
        {
            this.sendResponse(clientSocket, this.errore_404, "404 Not Found", "text/html");
        }

        private void forbidden(Socket clientSocket)
        {
            this.sendResponse(clientSocket, "403 Forbidden", "403 Forbidden", "text/html");
        }

        private void sendOkResponse(Socket clientSocket, byte[] bContent, string contentType)
        {
            this.sendResponse(clientSocket, bContent, "200 OK", contentType);
        }

        private void sendResponse(Socket clientSocket, string strContent, string responseCode, string contentType)
        {
            byte[] bytes = this.charEncoder.GetBytes(strContent);
            this.sendResponse(clientSocket, bytes, responseCode, contentType);
        }

        private void sendResponse(Socket clientSocket, byte[] bContent, string responseCode, string contentType)
        {
            try
            {
                byte[] bytes = this.charEncoder.GetBytes(string.Concat(new string[]
                {
                    "HTTP/1.1 ",
                    responseCode,
                    "\r\nServer: WebGive\r\nContent-Length: ",
                    bContent.Length.ToString(),
                    "\r\nConnection: close\r\nContent-Type: ",
                    contentType,
                    "\r\n\r\n"
                }));
                clientSocket.Send(bytes);
                clientSocket.Send(bContent);
                clientSocket.Close();
            }
            catch
            {
            }
        }
    }
}
