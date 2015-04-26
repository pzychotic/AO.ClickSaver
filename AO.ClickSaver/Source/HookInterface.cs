using System;
using pzy.AO.Hook;

namespace pzy.AO.ClickSaver
{
    public class HookInterface : IHookInterface
    {
        public override void IsInstalled( Int32 processId )
        {
        }

        public override void OnReceiveMessage( Int32 processId, Byte[] message )
        {
        }

        public override void ReportException( Int32 processId, Exception e )
        {
        }

        public override bool Ping( Int32 processId )
        {
            return true;
        }
    }
}
