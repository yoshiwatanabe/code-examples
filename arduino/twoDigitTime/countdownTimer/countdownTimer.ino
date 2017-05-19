#include "SevenSeg.h"

//Seven Seg
SevenSeg disp (10, 9, 8, 7, 6, 11, 12); //Defines the segments A-G: SevenSeg(A, B, C, D, E, F, G);
const int numOfDigits = 2;      //number of 7 segments
int digitPins [numOfDigits] = {4, 5}; //CC(or CA) pins of segment


//Constants
const int startButtonPin = 2;

//Variables
String numberText = "99";
int digit1 = 0;
int digit2 = 0;
int start;
int countValue;
//Useful flags
boolean countingDown = false;

void setup()
{
  pinMode(startButtonPin, INPUT_PULLUP);

  //Defines the number of digits to be "numOfDigits" and the digit pins to be the elements of the array "digitPins"
  disp.setDigitPins (numOfDigits , digitPins);

  //Only for common cathode 7segments
  disp.setCommonCathode();

  //Control brightness (values 0-100);
  disp.setDutyCycle(20);
  disp.setTimer(2);
  disp.startTimer();
}

void loop()
{
  //Read buttons state
  start = digitalRead(startButtonPin);
  
  //Start counting...
  if (start == LOW)
  {
    delay(500);
    if (!countingDown)
    {
      countingDown = true;
      countValue = numberText.toInt();
    }
  }
    
  
  //////////Counter Control///////////
  if (countingDown)
  {
    if (countValue > 0 && countValue <= 10)
    {
      countValue--;
      delay(1000);
      disp.write("0" + String(countValue));
    }
    else if (countValue > 10)
    {
      countValue--;
      delay(1000);
      disp.write(String(countValue));
    }
    else if (countValue == 0)
    {
      disp.write("00");
      delay(500);
      disp.write(" ");
      delay(500);
      countingDown = false;
    }
  }
  else
  {
    disp.write(numberText);
  }
}

ISR( TIMER2_COMPA_vect ) {
  disp.interruptAction ();
}


