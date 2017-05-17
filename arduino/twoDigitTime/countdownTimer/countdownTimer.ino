/*   Arduino two digit 7 Segment Display Counterdown Timer
  Dev: Michalis Vasilakis // Date: 30/3/2016 // Ver.1
  Find more info at www.ardumotive.com          */

//Library
#include "SevenSeg.h"

//Seven Seg
SevenSeg disp (10, 9, 8, 7, 6, 11, 12); //Defines the segments A-G: SevenSeg(A, B, C, D, E, F, G);
const int numOfDigits = 2;      //number of 7 segments
int digitPins [numOfDigits] = {4, 5}; //CC(or CA) pins of segment

//Constants
const int startButtonPin = 2;
const int changeButtonPin = 3;
const int setButtonPin = 13;
const int dotPoint1 = A0; //left digit
const int dotPoint2 = A1; //right digit
//Variables
String numberText = "99";
int digit1 = 0;
int digit2 = 0;
int start, changeState, set;
int setPoint = 0;
int countValue;
//Useful flags
boolean countingDown = false;

void setup()
{
  pinMode(startButtonPin, INPUT_PULLUP);
  pinMode(changeButtonPin, INPUT_PULLUP);
  pinMode(setButtonPin, INPUT_PULLUP);
  pinMode(dotPoint1, OUTPUT);
  pinMode(dotPoint2, OUTPUT);
  //Defines the number of digits to be "numOfDigits" and the digit pins to be the elements of the array "digitPins"
  disp.setDigitPins ( numOfDigits , digitPins );
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
  changeState = digitalRead(changeButtonPin);
  set = digitalRead(setButtonPin);

  if (set == LOW && !countingDown)
  {
    delay(500);
    if (setPoint < 2)
    {
      setPoint++;
    }
    else
    {
      setPoint = 0;
    }
  }

  //Start counting...
  if (start == LOW && setPoint == 0)
  {
    delay(500);
    if (!countingDown)
    {
      countingDown = true;
      countValue = numberText.toInt();
    }
    else
    {
      countingDown = false;
    }
  }
  
  if (changeState == LOW && setPoint == 1 && !countingDown) 
  {
    delay(500);
    if (digit1 < 9)
    {
      digit1++;
    }
    else 
    {
      digit1 = 0;
    }
  
    numberText = String(digit2) + String(digit1);
  }
  
  if (changeState == LOW && setPoint == 2 && !countingDown) 
  {
    delay(500);
    if (digit2 < 9) 
    {
      digit2++;
    }
    else 
    {
      digit2 = 0;
    }
    
    numberText = String(digit2) + String(digit1);
  }
  
  //Control dot points
  if (setPoint == 0) 
  {
    digitalWrite(dotPoint1, LOW);
    digitalWrite(dotPoint2, LOW);
  }
  else if (setPoint == 1) 
  {
    digitalWrite(dotPoint1, HIGH);
    digitalWrite(dotPoint2, LOW);
  }
  else if (setPoint == 2) 
  {
    digitalWrite(dotPoint1, LOW);
    digitalWrite(dotPoint2, HIGH);
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


