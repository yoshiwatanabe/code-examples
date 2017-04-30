package yoshi;

import javax.ejb.Stateless;

/**
 * Session Bean implementation class HelloImpl
 */
@Stateless
public class HelloImpl implements Hello {

    /**
     * Default constructor. 
     */
    public HelloImpl() {
        // TODO Auto-generated constructor stub
    }
    
    @Override
    public String sayHello(String msg) {
        return "Hello " + msg;
    }

}
