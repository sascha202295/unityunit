//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Input_ActionSet_Actions p_Actions;
        
        public static SteamVR_Input_ActionSet_Actions Actions
        {
            get
            {
                return SteamVR_Actions.p_Actions.GetCopy<SteamVR_Input_ActionSet_Actions>();
            }
        }
        
        private static void StartPreInitActionSets()
        {
            SteamVR_Actions.p_Actions = ((SteamVR_Input_ActionSet_Actions)(SteamVR_ActionSet.Create<SteamVR_Input_ActionSet_Actions>("/actions/Actions")));
            Valve.VR.SteamVR_Input.actionSets = new Valve.VR.SteamVR_ActionSet[] {
                    SteamVR_Actions.Actions};
        }
    }
}
