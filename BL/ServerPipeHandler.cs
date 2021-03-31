﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BL.Pipes;

namespace BL
{
    public class ServerPipeHandler: IDisposable
    {
        int pipeHandle;

        Thread t;

        bool sendCalled;
        private bool _cont;

        public void Dispose()
        {
            _cont = false;
            t?.Abort();
            if (pipeHandle != -1)
                Import.CloseHandle(pipeHandle);
        }

        public void Init()
        {
            pipeHandle = Import.CreateNamedPipe(
                "\\\\.\\pipe\\ParkingFinePipe",
                Types.PIPE_ACCESS_DUPLEX,
                Types.PIPE_TYPE_BYTE,
                Types.PIPE_UNLIMITED_INSTANCES,
                0,
                1024,
                Types.NMPWAIT_WAIT_FOREVER,
                (uint)0);
            _cont = true;
            t = new Thread(Sender);
            t.Start();
        }

        public void Send()
        {
            sendCalled = true;
        }

        void Sender()
        {
            while (_cont)
            {
                if (Import.ConnectNamedPipe(pipeHandle, 0))
                {
                    byte[] buffWrite = new byte[] { 0 };

                    if (sendCalled)
                    {
                        buffWrite = new byte[] { 1 };
                        sendCalled = false;
                    }

                    uint realBytesWrited = 0;

                    Import.WriteFile(pipeHandle, buffWrite, (uint)buffWrite.Length, ref realBytesWrited, 0);


                    Import.DisconnectNamedPipe(pipeHandle);                             // отключаемся от канала клиента 

                    // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
                }
                Thread.Sleep(100);
            }
        }

    }
}
