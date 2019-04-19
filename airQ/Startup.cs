using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;


using airQ.App_Code;
using System.Data.SqlClient;
using Newtonsoft.Json;

// including the M2Mqtt Library
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

[assembly: OwinStartup(typeof(airQ.Startup))]

namespace airQ
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            mqttConection();
        }
        public void mqttConection()
        {
            MqttClient client;
            string clientId;

            String[] mqtt_server = { "157.230.174.83", "test.mosquitto.org", "mqtt.fluux.io" };

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
                    if(dr.HasRows)
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
                foreach(string topic in topics)
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
                catch(Exception error)
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
            string topic = e.Topic;
            //string broker = sender.
            try
            {
                string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
                onmotica.saveIntoDB(ReceivedMessage, topic);                    
            }
            catch (Exception error)
            {
                onmotica.saveInLogMQTT(error);
            }
            finally
            {

            }
        }
    }
    
}
