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

            string BrokerAddress = onmotica.getBrokerAddress();

            client = new MqttClient(BrokerAddress);

            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();


            client.Connect(clientId);

            client.Subscribe(new String[] { "#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        }


        // this code runs when a message was received
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            string topic = e.Topic;
            var pSQL = "INSERT INTO [measurements] ([topic], [data], [registerAt], [activ], @otherFields) VALUES ('@topic', '@data', @registerAt, '@activ', @otherValues)";

            try
            {
                dynamic jsonMesssage = JsonConvert.DeserializeObject(ReceivedMessage);
                var data = "";
                pSQL = pSQL.Replace("@topic", topic);
                pSQL = pSQL.Replace("@registerAt", "GETDATE()");
                pSQL = pSQL.Replace("@activ", "1");

                data += jsonMesssage.D1; //temperatura - Cama caliente
                data += ",";
                data += jsonMesssage.D2; //humedad - Extrusor
                data += ",";
                data += jsonMesssage.D3; //Presion atmosferica - Motor 1
                data += ",";
                data += jsonMesssage.D4; //Alcoholes - Motor 2
                data += ",";
                data += jsonMesssage.D5; //TVOC - Motor 3
                data += ",";
                data += jsonMesssage.D6; //CO2 - Motor 4
                data += ",";
                data += jsonMesssage.D7; //Gas metano - Motor 5
                data += ",";
                data += jsonMesssage.D8; //Latitud - Corriente
                data += ",";
                data += jsonMesssage.D9; //Longitud - Voltaje

                if (topic.Contains("dron") & Convert.ToInt32(jsonMesssage.D1)>0)
                {
                    //airQ

                    var otherFields = "[temperatura], [humedad], [presionAtmosferica], [Alcohol], [TVOC], [CO2], [NH4], [Latitud], [Longitud], [fecha]";
                    pSQL = pSQL.Replace("@otherFields", otherFields);
                    var otherValues = data;
                    pSQL = pSQL.Replace("@otherValues", otherValues);

                    pSQL = pSQL.Replace("@data", data);
                    onmotica.executeSQLAirQ(pSQL);

                }
                else if (topic.Contains("printer") & Convert.ToInt32(jsonMesssage.D1) > 0)
                {
                    //3DPrinterSupervisionSys
                    data += jsonMesssage.D10; // - Potencia electrica

                    var otherFields = "[tempHotBed], [TempExt], [M1], [M2], [M3], [M4], [M5], [Corriente], [Voltaje], [PotenciaElectrica]";
                    pSQL = pSQL.Replace("@otherFields", otherFields);
                    var otherValues = data;
                    pSQL = pSQL.Replace("@otherValues", otherValues);

                    pSQL = pSQL.Replace("@data", data);
                    onmotica.executeSQLMonitor3D(pSQL);
                }
            }
            catch (Exception err)
            {
                //("Error insertando el registro -- ", err);
            }
            finally
            {

            }
            //txtReceived.Text = ReceivedMessage;
            //var context = GlobalHost.ConnectionManager.GetHubContext<dashboardHub>();

            //context.Clients.All.updateInfo(ReceivedMessage);
            //Response.Redirect("/dashboard");
        }
    }
    
}
