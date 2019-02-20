using System;

namespace ZZ_ERP.Infra.CrossCutting.Connections.Connections
{
    public class DelegateAction
    {
        public Action<object[], object[]> act;
        public Object[] local = new Object[3];
        public Object[] server = new Object[3];

        public void Exec(){
            act(server,local);
        }
    }
}