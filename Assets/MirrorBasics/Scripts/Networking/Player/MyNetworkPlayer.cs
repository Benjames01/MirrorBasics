using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColourRenderer = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField] private string displayName = "Missing Name";
    [SyncVar(hook = nameof(HandleDisplayColourUpdated))]
    [SerializeField] private Color teamColour = new Color();


    #region Server.
    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetTeamColour(Color newTeamColour)
    {
        teamColour = newTeamColour;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (!IsValidName(newDisplayName)) return;
        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }
    
    #endregion

    #region Client.

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log($"Setting display name to {newDisplayName}");
    }
    
    private void HandleDisplayColourUpdated(Color oldColour, Color newColour)
    {
        displayColourRenderer.material.SetColor("_Color", newColour);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("M");
    }

    #endregion


    #region Utilities

    public static bool IsValidName(string nameInput)
    {
        bool isValid = true;
        isValid = !string.IsNullOrEmpty(nameInput)
                  && Regex.IsMatch(nameInput, @"^[a-zA-Z]+$")
                  && nameInput.Length > 2;
        return isValid;
    }
    

    #endregion
}
