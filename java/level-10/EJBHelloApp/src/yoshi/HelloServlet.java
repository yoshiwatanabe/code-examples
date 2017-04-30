package yoshi;
import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.annotation.WebServlet;

import javax.ejb.EJB;

import yoshi.Hello;

@WebServlet("/hello")
public class HelloServlet extends HttpServlet {
    private static final long serialVersionUID = 1L;

    @EJB
    private Hello hello;

    protected void doGet(
        HttpServletRequest request,
        HttpServletResponse response
    ) throws ServletException, IOException {
        String msg = hello.sayHello("from local");
        request.setAttribute("msg", msg);

        getServletContext()
            .getRequestDispatcher("/hello.jsp")
            .forward(request, response);
    }
}
