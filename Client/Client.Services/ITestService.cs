// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.ServiceModel;

namespace Client.Services
{
    /// <summary>
    /// Hello temp.
    /// </summary>
    [ServiceContract]
    public interface ITestService
    {
        /// <summary>
        /// Temp hello.
        /// </summary>
        [OperationContract]
        void Hello();
    }
}