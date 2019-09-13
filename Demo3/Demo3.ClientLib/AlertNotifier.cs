using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo3.ClientLib
{
    public class AlertEventArgs : EventArgs
    {
        public AlertEventArgs(Data data)
        {
            Data = data;
        }

        public Data Data { get; }
    }

    public class AlertNotifier : IDisposable
    {
        public event EventHandler<AlertEventArgs> Alert;

        private HubConnection _connection;
        public async Task InitializeAsync()
        {
            _connection = new HubConnectionBuilder().WithUrl("http://localhost:7071/api/")
                .Build();
            _connection.On<Data>("onAlert", x => Alert?.Invoke(this, new AlertEventArgs(x)));
            _connection.Closed += async ex =>
            {
                await ConnectAsync();
            };

            await ConnectAsync();
        }

        private async Task ConnectAsync()
        {
            while (true)
            {
                try
                {
                    await _connection.StartAsync();
                    return;
                }
                catch
                {
                    await Task.Delay(3000);
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection?.DisposeAsync().Wait();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
