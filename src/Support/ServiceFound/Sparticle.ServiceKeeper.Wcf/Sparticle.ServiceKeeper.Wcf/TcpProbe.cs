using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace Sparticle.ServiceKeeper.Wcf
{
    internal class TcpProbe
    {
        public bool CanConnect(string host, int port, int millisecondsTimeout)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool connected = false;

            try
            {
                var tcpclient = new TcpClient(host, port);

                connected = tcpclient.Connected;

                tcpclient.Close();
            }
            catch(Exception ex)
            {
                connected = false;
            }
            finally
            {
                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > millisecondsTimeout)
                    connected = false;
            }

            return connected;
        }

        public bool CanSendMessage(string host, int port, int millisecondsTimeout, byte[] msg)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool sent = false;

            try
            {
                var tcpclient = new TcpClient(host, port);

                sent = tcpclient.Connected;

                if (sent)
                {
                    tcpclient.SendTimeout = millisecondsTimeout;

                    tcpclient.GetStream().Write(msg, 0, msg.Length);
                }

                tcpclient.Close();
            }
            catch (Exception ex)
            {
                sent = false;
            }
            finally
            {
                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > millisecondsTimeout)
                    sent = false;
            }

            return sent;
        }
    }
}