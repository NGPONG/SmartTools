<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Test_03.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">
         function Expand(obj) {
             if (!obj.savesize) obj.savesize = obj.size;
             obj.size = Math.max(obj.savesize, obj.value.length);
         }
  </script>
</head>
<body>
    <form>
       <input  type="text" size="5" style="font-family:Courier;" onkeyup="Expand(this);">
     </form>
</body>
</html>
