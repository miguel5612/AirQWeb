using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;


using airQ.App_Code;
using System.Data.SqlClient;
using Newtonsoft.Json;

// including the M2Mqtt Library
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;


namespace airQ
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //mqttConection();
        }
        public void mqttConection()
        {
            MqttClient client;
            string clientId;

            string BrokerAddress = onmotica.getBrokerAddress();

            client = new MqttClient(BrokerAddress);

            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceivedByAdmin;

            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();


            client.Connect(clientId);

            client.Subscribe(new String[] { "#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        }


        // this code runs when a message was received
        void client_MqttMsgPublishReceivedByAdmin(object sender, MqttMsgPublishEventArgs e)
        {
           string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            string topic = e.Topic;
            onmotica.saveIntoDB(ReceivedMessage, topic);
            //txtReceived.Text = ReceivedMessage;
            //var context = GlobalHost.ConnectionManager.GetHubContext<dashboardHub>();

            //context.Clients.All.updateInfo(ReceivedMessage);
            //Response.Redirect("/dashboard");
        }
    }
}