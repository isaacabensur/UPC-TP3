﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="GestionarFilmacion.aspx.cs" Inherits="GestionarFilmacion" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" />


     <script type = "text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
         }


         $(function () {
             $("[id$=txtDate]").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 buttonImage: '/img/calendar.jpg',
                 dateFormat:'dd/mm/yy'
                 
             });
         });

     
    </script>



    <div id="contenedor">
    <h2>GESTIONAR FILMACION</h2>
    <br />
        <fieldset>
        <legend style="font-size:14px">Búsqueda:</legend>
            <asp:Label ID="Label1" runat="server" Text="Fecha:"></asp:Label>
            <asp:TextBox ID="txtDate" runat="server" ReadOnly = "true"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                onclick="btnBuscar_Click" />
    
                <br /><br />
       </fieldset>        
        <asp:GridView ID="grvDetalle" AutoGenerateColumns="False" AllowPaging="True" runat="server" 
            OnRowCommand="grvDetalle_RowCommand" 
            GridLines="None"
            CssClass="mGrid"
            DataKeyNames="CodSalida,Estado,solFilmacion,inicioGrab,finGrab,rutaVideo"
            PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt">  
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <Columns>
        <asp:BoundField HeaderText="Fecha Actual" DataField="FechaActual">
            <ItemStyle HorizontalAlign="Center" Width="175px" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Hora de Salida" DataField="HoraSalida">
            <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:BoundField>
         <asp:BoundField HeaderText="Cod Salida" DataField="CodSalida">
            <ItemStyle HorizontalAlign="Left"  Width="80px" />
        </asp:BoundField>
        <asp:BoundField HeaderText="Hora de Destino" DataField="HoraDestino">
        <ItemStyle HorizontalAlign="Left" Width="80px"/>
        </asp:BoundField>
        <asp:BoundField HeaderText="Pasajeros" DataField="CantPasajeros">
            <ItemStyle HorizontalAlign="Left"  Width="90px" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="opcion">
            <ItemTemplate>


                <asp:ImageButton ID="ibtnConfirmar" runat="server" CausesValidation="false" CommandName="cmdProgramar" 
                   
                    ImageUrl="~/img/ok.jpg" ToolTip="Programar Filmacion"  visible='<%# Eval("Estado").ToString().Equals("") %>'
                    CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>'  />
                    &nbsp;
               <asp:ImageButton ID="ibtnCancelar" runat="server" CausesValidation="false" CommandName="cmdModificar"
                    
                    ImageUrl="~/img/edit.png" ToolTip="Modificar Filmacion" 
                    CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>' visible='<%# Eval("Estado").ToString().Equals("P") %>'/>
                    &nbsp;
               <asp:ImageButton ID="ibtnEditar" runat="server" CausesValidation="false" CommandName="cmdCopia"
                   
                    ImageUrl="~/img/print.jpg" ToolTip="Gestionar Copia" 
                    CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>' visible='<%# Eval("Estado").ToString().Equals("A") %>'/>
                
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="140px" />
        </asp:TemplateField>

        </Columns>      
            <EmptyDataTemplate>
                No se encontraron registros.
            </EmptyDataTemplate>            

<PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>  
        
         <asp:Button ID="btnImprimir" runat="server" CssClass="mybtnstyle" Text="Recomendaciones para Portar Equipajes" OnClientClick = "return PrintPanel();" />
         <asp:Button ID="btnInicio" runat="server" CssClass="mybtnstyle" 
            Text="Ir al Inicio" onclick="btnInicio_Click"/>

        <asp:Panel ID="pnlContents" runat="server">
         <div id="impresion">        
        <h3>Información de Equipaje</h3>
        <hr />
        <span style="font-size: 10pt; font-family:Arial Narrow">Estimado(a) Pasajero(a) <asp:Label
            ID="lblPasajero" runat="server" ></asp:Label>
            <br /><br />
            <span style="color: #18B5F0">Formalidad de viaje </span><br />
Necesitará un pasaporte válido para al menos los 6 meses próximos. Deberá rellenar un formulario de aduana en la entrada y salida del país. Los visados no son necesarios para los ciudadanos europeos. La estancia no puede superar 3 meses. Para los coches, la tarjeta verde es válida en Marruecos. Si su seguro no cubre el país, deberá contratar un seguro con cobertura adicional. No hace falta ninguna vacuna especial para los viajeros procedentes de Europa.
<br />
Cambio de horario
Marruecos está a hora GMT. No hay cambio de hora en verano.
<br /><br />
<span style="color: #18B5F0">Modo de pago y propinas </span><br />
El dirham es una moneda no convertible fuera del país. Puede consultar el equivalente de la moneda en: www.lecenomiste.com. Los cajeros automáticos con pegatinas VISA o MAESTRO aceptan sus tarjetas bancarias. Se encuentran en todas las grandes ciudades. Los bancos y grandes hoteles tienen oficina de cambio. En Tánger, es posible cambiar los euros en las tiendas de comestibles, estancos y otras tiendas. La mayoría de las tarjetas bancarias se aceptan en los hoteles, restaurantes y tiendas de lujo. Las propinas son una norma general en Marruecos. Están consideradas como un extra sobre el salario.
<br /><br />
<span style="color: #18B5F0">Telecomunicación y electricidad </span><br />
Los móviles funcionan en Marruecos. Consulte con su operador si su contrato cubre los países del maghreb. Para el acceso a Internet, no dude en entrar en un cybercafe. Hay muchos en Marruecos, la mayoría están equipados con ADSL y cuesta alrededor de 10dhs (+- 1) por 1 hora. La corriente eléctrica es de 220 voltios, y los enchufes son de tipo europeos.
<br /><br />
<span style="color: #18B5F0">Estar en Marruecos </span><br />
Tierra de acogimiento y de tolerancia, Marruecos no deja de ser un país apegado a sus tradiciones. Puede esperar una comunicación fácil y agradable con los marroquíes. Aún así, actitud y ropa respetuosa son elementales en tierra islámica.</span>
        </div>
        </asp:Panel>
       
             
    </div>
</asp:Content>
