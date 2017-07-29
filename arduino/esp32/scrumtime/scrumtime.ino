/*
 WiFi Web Server LED Blink

 A simple web server that lets you blink an LED via the web.
 This sketch will print the IP address of your WiFi Shield (once connected)
 to the Serial monitor. From there, you can open that address in a web browser
 to turn on and off the LED on pin 5.

 If the IP address of your shield is yourAddress:
 http://yourAddress/H turns the LED on
 http://yourAddress/L turns it off

 This example is written for a network using WPA encryption. For
 WEP or WPA, change the Wifi.begin() call accordingly.

 Circuit:
 * WiFi shield attached
 * LED attached to pin 5

 created for arduino 25 Nov 2012
 by Tom Igoe

ported for sparkfun esp32 
31.01.2017 by Jan Hendrik Berlin
 
 */

#include <WiFi.h>
#include <HTTPClient.h>

const char* ssid     = "ATT-WIFI-8775";
const char* password = "58271517";

const int ledPin =  13;      // the number of the LED pin
const int buttonPin = 2;     // the number of the pushbutton pin
const int buttonPin4 = 4;

int buttonState2Prev = 0;
int buttonState4Prev = 0;
int buttonState = 0;         // variable for reading the pushbutton status
int buttonState4 = 0; 

void setup() {
  Serial.begin(115200);
  delay(10);
  Serial.print("\nConnecting to "); Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(WiFi.status());
  }
  Serial.print("\nWiFi connected   IP: "); Serial.println(WiFi.localIP());


  // initialize the LED pin as an output:
 // pinMode(ledPin, OUTPUT);
    // initialize the pushbutton pin as an input:
  pinMode(buttonPin, INPUT);
  pinMode(buttonPin4, INPUT);
}



void loop() {
 
 buttonState2Prev = buttonState;
 buttonState4Prev = buttonState4;
 buttonState = digitalRead(buttonPin);
 buttonState4 = digitalRead(buttonPin4);

   // check if the pushbutton is pressed. If it is, the buttonState is HIGH:
  if (buttonState2Prev == LOW && buttonState == HIGH) {
    // turn LED on:
  //  digitalWrite(ledPin, HIGH);
    Serial.print("HIGH\n");
  } else {
    // turn LED off:
  //  digitalWrite(ledPin, LOW);
    //Serial.print("LOW\n");
  }


    if (buttonState4Prev == LOW && buttonState4 == HIGH) {
    // turn LED on:
  //  digitalWrite(ledPin, HIGH);
    Serial.print("HIGH 4\n");
  } else {
    // turn LED off:
  //  digitalWrite(ledPin, LOW);
    //Serial.print("LOW 4\n");
  }

   delay(100);


//  if(WiFi.status()== WL_CONNECTED){   //Check WiFi connection status
 
//    HTTPClient http;   
 
//    http.begin("http://posttestserver.com/post.php");  //Specify destination for HTTP request
//    http.addHeader("Content-Type", "text/plain");             //Specify content-type header
 
//    int httpResponseCode = http.POST("POSTING from ESP32");   //Send the actual POST request
 
//    if(httpResponseCode>0){
 
//     String response = http.getString();                       //Get the response to the request
 
//     Serial.println(httpResponseCode);   //Print return code
//     Serial.println(response);           //Print request answer
 
//    }else{
 
//     Serial.print("Error on sending POST: ");
//     Serial.println(httpResponseCode);
 
//    }
 
//    http.end();  //Free resources
 
//  }else{
 
//     Serial.println("Error in WiFi connection");   
 
//  }
 
//   delay(100000);  //Send a request every 10 seconds
 
}
