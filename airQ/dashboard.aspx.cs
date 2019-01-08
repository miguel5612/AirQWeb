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
        public void mqttConection()
        {
            MqttClient client;
            string clientId;

            string BrokerAddress = onmotica.getBrokerAddress();

            client = new MqttClient(BrokerAddress);

            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();


            client.Connect(clientId);

            String inTopic = Session["inTopic"].ToString();
            client.Subscribe(new String[] { inTopic  }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        }


        // this code runs when a message was received
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
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