<%@ Page Language="C#" %>
<HTML>
	<body>
		<h1>Protected Page</h1>
		<hr>
		<br>
		<% Response.Write (Context.User.Identity.Name + ": "); %>
	</body>
</HTML>
