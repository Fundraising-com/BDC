<%	
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
' EZExpirePageInclude.asp
'
' NOTE: INSERT THIS INCLUDE FILE AT THE TOP OF ANY PAGE 
'		YOU WISH TO BE A NON-CACHEABLE PAGE.
'
' In the beginning, the Web was a friendly place where a user
' requested a page and the Web server delivered that page.
' Now, proxy servers have invaded the Web and attempt to cache 
' every page on the www, causing headaches everywhere.  In our 
' case, the proxy servers are not only caching the pages, but 
' cookie and session information as well.  Ouch!
'
' What does all this mean?  When a subscriber requests a page, 
' it is sometime cached on the proxy server, along with their
' session information.  If a different subscriber, or even a
' non-subscriber comes along a short time later and requests
' the same page, they may be served up the cached version
' (including the original subscribers session info).  NOT GOOD!
' Subscribers are seeing other subscribers messages.
'
' What can we do about it?  A majority of the complaints are
' coming from AOL users, so the AOL proxy servers seem to be
' the biggest problem child.  Per AOL's documentation, we are
' going to have to insert the following lines in each one of
' our pages to try and prevent their proxy servers from caching
' our pages.  What a pain!
'
' The following pages will not be cached by AOL:
'		* https:\\ requests
'		* HTTP POST requests
'		* pages with error status code
' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
Response.CacheControl="private"
Response.Expires = -1441
%>