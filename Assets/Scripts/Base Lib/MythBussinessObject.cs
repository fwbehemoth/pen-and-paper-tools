using LitJson;
using UnityEditor;

namespace Myth.BaseLib {
    public class MythBussinessObject {

        //Fetch from server
        public virtual void fetch() {
            Globals.Instance().DebugLog(this.GetType().Name,"Forgt to override");
        }

        //send to server
        public virtual void save() {
            Globals.Instance().DebugLog(this.GetType().Name,"Forgt to override");
        }

        //Success Callback
        protected virtual void success(JsonData json) {
            Globals.Instance().DebugLog(this.GetType().Name, json.ToString());
        }

        //Failure Callback
        protected virtual void failure(string error) {
            Globals.Instance().DebugLog(this.GetType().Name, error);
        }
    }
}