﻿namespace BloomService.Web.Infrastructure.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;

    public class PushNotification
    {
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private TcpClient _apnsClient;
        private SslStream _apnsStream;
        private X509Certificate _certificate;
        private X509CertificateCollection _certificates;

        public string P12File { get; set; }
        public string P12FilePassword { get; set; }


        // Default configurations for APNS
        private const string ProductionHost = "gateway.push.apple.com";
        private const string SandboxHost = "gateway.sandbox.push.apple.com";
        private const int NotificationPort = 2195;

        // Default configurations for Feedback Service
        private const string ProductionFeedbackHost = "feedback.push.apple.com";
        private const string SandboxFeedbackHost = "feedback.sandbox.push.apple.com";
        private const int FeedbackPort = 2196;


        private bool _conected = false;

        private readonly string _host;
        private readonly string _feedbackHost;

        private List<NotificationPayload> _notifications = new List<NotificationPayload>();
        private List<string> _rejected = new List<string>();

        private Dictionary<int, string> _errorList = new Dictionary<int, string>();


        public PushNotification(bool useSandbox, string p12File, string p12FilePassword)
        {
            if (useSandbox)
            {
                this._host = SandboxHost;
                this._feedbackHost = SandboxFeedbackHost;
            }
            else
            {
                this._host = ProductionHost;
                this._feedbackHost = ProductionFeedbackHost;
            }

            //Load Certificates in to collection.
            this._certificate = string.IsNullOrEmpty(p12FilePassword) ? new X509Certificate2(File.ReadAllBytes(p12File)) : new X509Certificate2(File.ReadAllBytes(p12File), p12FilePassword);
            this._certificates = new X509CertificateCollection { this._certificate };

            // Loading Apple error response list.
            this._errorList.Add(0, "No errors encountered");
            this._errorList.Add(1, "Processing error");
            this._errorList.Add(2, "Missing device token");
            this._errorList.Add(3, "Missing topic");
            this._errorList.Add(4, "Missing payload");
            this._errorList.Add(5, "Invalid token size");
            this._errorList.Add(6, "Invalid topic size");
            this._errorList.Add(7, "Invalid payload size");
            this._errorList.Add(8, "Invalid token");
            this._errorList.Add(255, "None (unknown)");
        }

        public List<string> SendToApple(List<NotificationPayload> queue)
        {
            //Logger.Info("Payload queue received.");
            this._notifications = queue;
            if (queue.Count < 8999)
            {
                this.SendQueueToapple(this._notifications);
            }
            else
            {
                const int pageSize = 8999;
                int numberOfPages = (queue.Count / pageSize) + (queue.Count % pageSize == 0 ? 0 : 1);
                int currentPage = 0;

                while (currentPage < numberOfPages)
                {
                    this._notifications = (queue.Skip(currentPage * pageSize).Take(pageSize)).ToList();
                    this.SendQueueToapple(this._notifications);
                    currentPage++;
                }
            }
            //Close the connection
            this.Disconnect();
            return this._rejected;
        }

        private void SendQueueToapple(IEnumerable<NotificationPayload> queue)
        {
            int i = 1000;
            foreach (var item in queue)
            {
                if (!this._conected)
                {
                    this.Connect(this._host, NotificationPort, this._certificates);
                    var response = new byte[6];
                    this._apnsStream.BeginRead(response, 0, 6, this.ReadResponse, new MyAsyncInfo(response, this._apnsStream));
                }
                try
                {
                    if (item.DeviceToken.Length == 64) //check lenght of device token, if its shorter or longer stop generating Payload.
                    {
                        item.PayloadId = i;
                        byte[] payload = GeneratePayload(item);
                        this._apnsStream.Write(payload);
                        //Logger.Info("Notification successfully sent to APNS server for Device Toekn : " + item.DeviceToken);
                        Thread.Sleep(1000); //Wait to get the response from apple.
                    }
                    //else
                    //   // Logger.Error("Invalid device token length, possible simulator entry: " + item.DeviceToken);
                }
                catch (Exception ex)
                {
                    //Logger.Error("An error occurred on sending payload for device token {0} - {1}", item.DeviceToken, ex.Message);
                    this._conected = false;
                }
                i++;
            }
        }

        private void ReadResponse(IAsyncResult ar)
        {
            if (!this._conected)
                return;
            string payLoadId = "";
            int payLoadIndex = 0;
            try
            {
                var info = ar.AsyncState as MyAsyncInfo;
                info.MyStream.ReadTimeout = 100;
                if (this._apnsStream.CanRead)
                {
                    var command = Convert.ToInt16(info.ByteArray[0]);
                    var status = Convert.ToInt16(info.ByteArray[1]);
                    var ID = new byte[4];
                    Array.Copy(info.ByteArray, 2, ID, 0, 4);

                    payLoadId = Encoding.Default.GetString(ID);
                    payLoadIndex = ((int.Parse(payLoadId)) - 1000);
                    //Logger.Error("Apple rejected palyload for device token : " + _notifications[payLoadIndex].DeviceToken);
                    //Logger.Error("Apple Error code : " + _errorList[status]);
                    //Logger.Error("Connection terminated by Apple.");
                    this._rejected.Add(this._notifications[payLoadIndex].DeviceToken);
                    this._conected = false;
                }
            }
            catch (Exception ex)
            {
                //Logger.Error("An error occurred while reading Apple response for token {0} - {1}", _notifications[payLoadIndex].DeviceToken, ex.Message);
            }
        }

        private void Connect(string host, int port, X509CertificateCollection certificates)
        {
            //Logger.Info("Connecting to apple server.");
            try
            {
                this._apnsClient = new TcpClient();
                this._apnsClient.Connect(host, port);
            }
            catch (SocketException ex)
            {
                //Logger.Error("An error occurred while connecting to APNS servers - " + ex.Message);
            }

            var sslOpened = this.OpenSslStream(host, certificates);

            if (sslOpened)
            {
                this._conected = true;
                //Logger.Info("Conected.");
            }

        }

        private void Disconnect()
        {
            try
            {
                Thread.Sleep(500);
                this._apnsClient.Close();
                this._apnsStream.Close();
                this._apnsStream.Dispose();
                this._apnsStream = null;
                this._conected = false;
                //Logger.Info("Disconnected.");
            }
            catch (Exception ex)
            {
                //Logger.Error("An error occurred while disconnecting. - " + ex.Message);
            }
        }

        private bool OpenSslStream(string host, X509CertificateCollection certificates)
        {
            //Logger.Info("Creating SSL connection.");
            this._apnsStream = new SslStream(this._apnsClient.GetStream(), false, this.validateServerCertificate, this.SelectLocalCertificate);

            try
            {
                this._apnsStream.AuthenticateAsClient(host, certificates, System.Security.Authentication.SslProtocols.Tls, false);
            }
            catch (System.Security.Authentication.AuthenticationException ex)
            {
                //Logger.Error(ex.Message);
                return false;
            }

            if (!this._apnsStream.IsMutuallyAuthenticated)
            {
                //Logger.Error("SSL Stream Failed to Authenticate");
                return false;
            }

            if (!this._apnsStream.CanWrite)
            {
                //Logger.Error("SSL Stream is not Writable");
                return false;
            }
            return true;
        }

        private bool validateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; // Dont care about server's cert
        }

        private X509Certificate SelectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            return this._certificate;
        }

        private static byte[] GeneratePayload(NotificationPayload payload)
        {
            try
            {
                //convert Devide token to HEX value.
                byte[] deviceToken = new byte[payload.DeviceToken.Length / 2];
                for (int i = 0; i < deviceToken.Length; i++)
                    deviceToken[i] = byte.Parse(payload.DeviceToken.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);

                var memoryStream = new MemoryStream();

                // Command
                memoryStream.WriteByte(1); // Changed command Type 

                //Adding ID to Payload
                memoryStream.Write(Encoding.ASCII.GetBytes(payload.PayloadId.ToString()), 0, payload.PayloadId.ToString().Length);

                //Adding ExpiryDate to Payload
                int epoch = (int)(DateTime.UtcNow.AddMinutes(300) - new DateTime(1970, 1, 1)).TotalSeconds;
                byte[] timeStamp = BitConverter.GetBytes(epoch);
                memoryStream.Write(timeStamp, 0, timeStamp.Length);

                byte[] tokenLength = BitConverter.GetBytes((Int16)32);
                Array.Reverse(tokenLength);
                // device token length
                memoryStream.Write(tokenLength, 0, 2);

                // Token
                memoryStream.Write(deviceToken, 0, 32);

                // String length
                string apnMessage = payload.ToJson();
                //Logger.Info("Payload generated for " + payload.DeviceToken + " : " + apnMessage);

                byte[] apnMessageLength = BitConverter.GetBytes((Int16)apnMessage.Length);
                Array.Reverse(apnMessageLength);

                // message length
                memoryStream.Write(apnMessageLength, 0, 2);

                // Write the message
                memoryStream.Write(Encoding.ASCII.GetBytes(apnMessage), 0, apnMessage.Length);
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                //Logger.Error("Unable to generate payload - " + ex.Message);
                return null;
            }
        }

        public List<Feedback> GetFeedBack()
        {
            try
            {
                var feedbacks = new List<Feedback>();
                //Logger.Info("Connecting to feedback service.");

                if (!this._conected)
                    this.Connect(this._feedbackHost, FeedbackPort, this._certificates);

                if (this._conected)
                {
                    //Set up
                    byte[] buffer = new byte[38];
                    int recd = 0;
                    DateTime minTimestamp = DateTime.Now.AddYears(-1);

                    //Get the first feedback
                    recd = this._apnsStream.Read(buffer, 0, buffer.Length);
                    //Logger.Info("Feedback response received.");

                    if (recd == 0)
                        //Logger.Info("Feedback response is empty.");

                    //Continue while we have results and are not disposing
                    while (recd > 0)
                    {
                        //Logger.Info("processing feedback response");
                        var fb = new Feedback();

                        //Get our seconds since 1970 ?
                        byte[] bSeconds = new byte[4];
                        byte[] bDeviceToken = new byte[32];

                        Array.Copy(buffer, 0, bSeconds, 0, 4);

                        //Check endianness
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bSeconds);

                        int tSeconds = BitConverter.ToInt32(bSeconds, 0);

                        //Add seconds since 1970 to that date, in UTC and then get it locally
                        fb.Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(tSeconds).ToLocalTime();


                        //Now copy out the device token
                        Array.Copy(buffer, 6, bDeviceToken, 0, 32);

                        fb.DeviceToken = BitConverter.ToString(bDeviceToken).Replace("-", "").ToLower().Trim();

                        //Make sure we have a good feedback tuple
                        if (fb.DeviceToken.Length == 64 && fb.Timestamp > minTimestamp)
                        {
                            //Raise event
                            //this.Feedback(this, fb);
                            feedbacks.Add(fb);
                        }

                        //Clear our array to reuse it
                        Array.Clear(buffer, 0, buffer.Length);

                        //Read the next feedback
                        recd = this._apnsStream.Read(buffer, 0, buffer.Length);
                    }
                    //clode the connection here !
                    this.Disconnect();
                    if (feedbacks.Count > 0)
                        //Logger.Info("Total {0} feedbacks received.", feedbacks.Count);
                    return feedbacks;
                }
            }
            catch (Exception ex)
            {
                //Logger.Error("Error occurred on receiving feed back. - " + ex.Message);
                return null;
            }
            return null;
        }
    }

    public class MyAsyncInfo
    {
        public Byte[] ByteArray { get; set; }
        public SslStream MyStream { get; set; }

        public MyAsyncInfo(Byte[] array, SslStream stream)
        {
            this.ByteArray = array;
            this.MyStream = stream;
        }
    }
}