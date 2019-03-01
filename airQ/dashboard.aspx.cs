using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;

// including the M2Mqtt Library
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;


//Libreria signalR
using airQ.Hubs;
using Microsoft.AspNet.SignalR;

//Qr Generator
using System.IO;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace airQ
{
    public partial class dashboard1 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            onmotica.isLogged(Session, Response,"dashboard");

        }
        void Page_LoadComplete(object sender, EventArgs e)
        {
            try
            {
                if (Session["deviceName"] != null & Session["deviceId"] != null)
                {
                    lblTittle.Text = Session["deviceName" + Session["deviceId"].ToString()].ToString();
                    divMeters.Visible = true;
                }
                else
                {
                    lblTittle.Text = "No tienes dispositivos registrados, haz clic en registrar nuevo dispositivo...";
                    divMeters.Visible = false;
                }
                var devId = Session["deviceId"].ToString();
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap imagen = encoder.Encode("https://airq.dronefenix.a2hosted.com/info?devId=" + devId);
                MemoryStream ms = new MemoryStream();
                imagen.Save(ms, ImageFormat.Gif);
                imgQR.Src = "data:image/gif;base64," + Convert.ToBase64String(ms.ToArray());

            mqttConection();

            }
            catch(Exception err)
            {

            }
        }
        public void mqttConection()
        {
            MqttClient client;
            string clientId;

            String[] mqtt_server = { "test.mosquitto.org", "iot.eclipse.org", "157.230.174.83" };

            int numTopic = 1;
            var pSQL = "SELECT COUNT(*) as total from devices";
            using (SqlDataReader dr = onmotica.fetchReader(pSQL))
            {
                while (dr.Read())
                {
                    if (dr.HasRows)
                    {
                        numTopic = (int)dr["total"];
                    }
                }
            }

            String[] topics = new string[numTopic];

            var counter = 0;
            pSQL = "SELECT inTopic from devices";
            using (SqlDataReader dr = onmotica.fetchReader(pSQL))
            {
                while (dr.Read())
                {
                    if (dr.HasRows)
                    {
                        topics[counter] = dr["inTopic"].ToString();
                        counter++;
                    }
                }
            }


            try
            {

                string BrokerAddress = mqtt_server[0];
                client = new MqttClient(BrokerAddress);
                // register a callback-function (we have to implement, see below) which is called by the library when a message was received
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived1;
                // use a unique id as client id, each time we start the application
                clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
                foreach (string topic in topics)
                {
                    client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                }

                BrokerAddress = mqtt_server[1];
                client = new MqttClient(BrokerAddress);
                // register a callback-function (we have to implement, see below) which is called by the library when a message was received
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived2;
                // use a unique id as client id, each time we start the application
                clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
                foreach (string topic in topics)
                {
                    client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                }

                BrokerAddress = mqtt_server[2];
                client = new MqttClient(BrokerAddress);
                // register a callback-function (we have to implement, see below) which is called by the library when a message was received
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived2;
                // use a unique id as client id, each time we start the application
                clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);
                foreach (string topic in topics)
                {
                    client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                }

            }
            catch (Exception error)
            {
                onmotica.saveInLogMQTT(error);
            }
            finally
            {

            }
        
    }


        // this code runs when a message was received
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            processMsg(e);
        }
        void client_MqttMsgPublishReceived1(object sender, MqttMsgPublishEventArgs e)
        {
            processMsg(e);
        }

        void client_MqttMsgPublishReceived2(object sender, MqttMsgPublishEventArgs e)
        {
            processMsg(e);
        }

        void client_MqttMsgPublishReceived3(object sender, MqttMsgPublishEventArgs e)
        {
            processMsg(e);
        }
        void client_MqttMsgPublishReceived4(object sender, MqttMsgPublishEventArgs e)
        {
            processMsg(e);
        }


        void processMsg(MqttMsgPublishEventArgs e)
        {
            var inTopic = e.Topic;
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            txtReceived.Text = ReceivedMessage;
            var context = GlobalHost.ConnectionManager.GetHubContext<dashboardHub>();

            context.Clients.All.updateInfo(ReceivedMessage, inTopic);
            //Response.Redirect("/dashboard");
        }

        protected void dayReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("/dayReport");
        }

        protected void weekReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("/weekReport");
        }

        protected void monthReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("/monthReport");
        }
    }
}