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

            mqttConection();
        }
        public void mqttConection()
        {
            MqttClient client;
            string clientId;

            string BrokerAddress = "68.183.31.237";

            client = new MqttClient(BrokerAddress);

            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();


            client.Connect(clientId);

            client.Subscribe(new String[] { "droneFenix/2/estacion1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        }


        // this code runs when a message was received
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            txtReceived.Text = ReceivedMessage;
            var context = GlobalHost.ConnectionManager.GetHubContext<dashboardHub>();

            context.Clients.All.updateInfo(ReceivedMessage);
            //Response.Redirect("/dashboard");
        }
    }
}