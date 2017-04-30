<!doctype html>
<html>
<head>
<title>JSP Test</title>
<%!
String message = "Hello, World.";
%>
</head>
<body>
<h2><%= message%></h2>
<%= new testlib.Util().getMessage() %>
</body>
</html>
