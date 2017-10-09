using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

    public interface IAzureDocClient
    {
        DocumentClient Client { get; }

        void InitializeClient();
    }