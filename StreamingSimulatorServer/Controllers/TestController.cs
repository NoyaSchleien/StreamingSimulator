using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StreamingSimulatorServer.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            ////This is a way to get the answer from the UDPClient. After I try to get the answer in a loop.
            ////Creates a UdpClient for reading incoming data.
            //UdpClient receivingUdpClient = new UdpClient(55002);

            ////Creates an IPEndPoint to record the IP Address and port number of the sender. 
            //// The IPEndPoint will allow you to read datagrams sent from any source.
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //try
            //{

            //    // Blocks until a message returns on this socket from a remote host.
            //    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
            //                        string returnData = Encoding.ASCII.GetString(receiveBytes);

            //        Console.WriteLine("This is the message you received " +
            //                                     returnData.ToString());
            //        Console.WriteLine("This message was sent from " +
            //                                    RemoteIpEndPoint.Address.ToString() +
            //                                    " on their port number " +
            //                                    RemoteIpEndPoint.Port.ToString());
            //        return Ok(returnData);
            //    }

            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //    return BadRequest("person not found");
            //}

            //Trying to listen and get the answer in a loop

        public static bool _stopNotifier = false;
        private void listener()
        {
            UdpClient udpClient = new UdpClient(new
            IPEndPoint(IPAddress.Any, 55002));
            try
            {
                udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), udpClient);
                while (!_stopNotifier)
                {
                    Task.Delay(1000).Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex = " + ex);
            }
        }
        public void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient udp = (UdpClient)(ar.AsyncState);
            IPEndPoint source = new IPEndPoint(0, 0);
            Byte[] receiveBytes = udp.EndReceive(ar, ref source);
            string returnData = Encoding.ASCII.GetString(receiveBytes);
            if (!_stopNotifier) udp.BeginReceive(new AsyncCallback(ReceiveCallback), udp);
        }

    }

}
}


