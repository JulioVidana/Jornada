<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Acceso a Sistemas</title>

    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="Content/css/animate.css" rel="stylesheet"/>
    <link href="Content/css/style.css" rel="stylesheet"/>

    <!-- Sweet Alert -->
    <link href="Content/css/plugins/sweetalert/sweetalert.css" rel="stylesheet"/>
    <!-- Chosen -->
    <link href="Content/css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet"/>

</head>
<body class="gray-bg">
    <form id="Login_form" runat="server">

        <div class="middle-box text-center loginscreen animated fadeInDown">
             
            <div >
                <div style="height:100px">
                    <asp:Image ID="Logo_Grande" runat="server" ImageUrl="Content/img/logo-Jornadas.png" />
                </div>
               <%-- <h3>Bienvenido al Portal de Captura de Jornadas</h3>--%>
                <p>Ingresa con tu usuario y contraseña.</p>
                <div class="form-group">
                    <asp:TextBox runat="server" ID="Usuario_TextBox" placeholder="usuario" required="true" MaxLength="150" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" ID="Password_TextBox" placeholder="contraseña" required="true" type="password" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button runat="server" ID="Ingresar_Button" Text="Ingresar" CssClass="btn btn-primary block full-width m-b" OnClick="Ingresar_Button_Click" />

               <%-- <a href="http://sistemas.difson.gob.mx/olvido_contrasena.aspx"><small>Olvido su Contraseña ?</small></a>--%>
            </div>
            
        </div>

        <!-- Mainly scripts -->
        <script src="Content/js/jquery-3.1.1.min.js"></script>
        <%--<script src="/content/js/popper.min.js"></script>--%>
        <script src="/content/js/bootstrap.js"></script>
        <!-- Sweet alert -->
        <script src="Content/js/plugins/sweetalert/sweetalert.min.js"></script>
        <!-- Chosen -->
        <script src="Content/js/plugins/chosen/chosen.jquery.js"></script>

        <script type="text/javascript">

            $(document).ready(function () {
                ejecuta_javascript();
            });

            function ejecuta_javascript() {
                $(".chosen-select").chosen({ width: '90%' });
            }

            function despliega_aviso(tipo, titulo, mensaje, txt_btn_no, txt_btn_si, txt_msj_no, txt_msj_si) {
                if (tipo == 'warning') {
                    swal({ title: titulo, text: mensaje, type: tipo, showCancelButton: false });
                }
                else if (tipo == 'pregunta') {
                    swal({ title: titulo, text: mensaje, type: "warning", showCancelButton: "true", confirmButtonColor: "#DD6B55", confirmButtonText: txt_btn_si, cancelButtonText: txt_btn_no, closeOnConfirm: false, closeOnCancel: false },
                        function (isConfirm) {
                            if (isConfirm) { despliega_aviso("normal", txt_msj_si, ""); }
                            else {
                                despliega_aviso("normal", txt_msj_no, "");
                                return false;
                            }
                        });
                }
                else if (tipo == 'success') { swal({ title: titulo, text: mensaje, type: tipo }); }
                else { swal({ title: titulo, text: mensaje }); }
            }

        </script>

    </form>

</body>
</html>
