<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CognitiveServiceApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3>Cognitive Service for Semi-Structured Data</h3>
        <div>
       
            <asp:Table ID="Table1" runat="server"
                Width="100%">
                <asp:TableRow>
                    <asp:TableCell Width="10%">
               <asp:Label runat="server" >Enter the query:</asp:Label>  
                        </asp:TableCell>
                   <asp:TableCell Width="100%">
               <asp:TextBox TextMode="MultiLine" ID="txtQuery" runat="server" Width="90%"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell >
                <asp:Button ID="btnSubmit" Text="Submit Query" OnClick="BtnSubmit_Click" runat="server" />
                    </asp:TableCell>
                
                </asp:TableRow>
            </asp:Table>
            
        </div>
        <div>
            <p>Result:</p>
            <asp:Label ID="txtResult" runat="server"></asp:Label>
            <asp:GridView ID="resultGrid" Width="100%" runat="server"></asp:GridView>
        </div>
    </div>

</asp:Content>
