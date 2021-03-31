using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BL.Pipes;

namespace BL
{
    public class ClientPipeHanlder: IDisposable
    {
        private bool _cont;
        private Thread thread;
        private int recieverHandler;

        public event Action UpdateCalled;

        public void Init()
        {
            StartThread();
        }
        void StartThread()
        {
            _cont = true;
            thread = new Thread(Reciever);
            thread.Start();
        }

        private void Reciever()
        {
            while (_cont)
            {
                recieverHandler = Import.CreateFile(
                    "\\\\.\\pipe\\ParkingFinePipe",
                    Types.EFileAccess.GenericRead,
                    Types.EFileShare.Read,
                    0,
                    Types.ECreationDisposition.OpenExisting,
                    0,
                    0);

                byte[] buff = new byte[1];
                uint bytesToRead = 0;

                if (Import.ReadFile(recieverHandler, buff, Convert.ToUInt32(buff.Length), ref bytesToRead, 0))         // выполняем запись последовательности байт в канал                
                {
                    if (buff[0] == 1)
                    {
                        UpdateCalled?.Invoke();
                    }
                }
                if (Import.CloseHandle(recieverHandler))
                {
                    recieverHandler = -1;
                }

                Thread.Sleep(100);
            }
        }

        public void Dispose()
        {
            _cont = false;
            thread?.Abort();

            if(recieverHandler != -1)
            {
                Import.CloseHandle(recieverHandler);
            }
        }
    }
}
