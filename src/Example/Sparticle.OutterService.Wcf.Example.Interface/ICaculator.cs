using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sparticle.OutterService.Wcf.Example.Interface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICaculator
    {
        [OperationContract]
        CalcResult Add(int num1, int num2);

        [OperationContract]
        CalcResult Div(int div, int divd);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CalcResult
    {
        [DataMember]
        public int Number;
    }
}
