package yoshi;

import javax.ejb.Local;

@Local
public interface Hello {
	String sayHello(String msg);
}
