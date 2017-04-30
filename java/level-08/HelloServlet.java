package test;

import java.io.*;
import javax.servlet.*;
import javax.servlet.http.*;

public class HelloServlet extends HttpServlet
{
   public void doGet (HttpServletRequest request, HttpServletResponse response )
      throws IOException, ServletException
   {
      response.setContentType( "text/html" );
      PrintWriter out = response.getWriter();

      out.println( "<HTML>Hello</HTML>" );
      out.close();
   }
}
