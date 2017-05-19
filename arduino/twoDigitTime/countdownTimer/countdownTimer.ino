#include "SevenSeg.h"

const int maxSeconds = 180;
const int oneMinuteInS = 60;
const int oneSecondInMS = 1000;
const int startButtonPin = 2;
const int statusOKIndicatorPin = 0;
const int statusWarningIndicatorPin = 0;
const int statusOverIndicatorPin = 0;
const int digitPinsCount = 2;

int digitPins [digitPinsCount] = {4, 5}; //CC(or CA) pins of segment
SevenSeg disp (10, 9, 8, 7, 6, 11, 12); //Defines the segments A-G: SevenSeg(A, B, C, D, E, F, G);
int counter;
boolean isCounting = false;
boolean canceled = false;

enum Status { StatusOK, StatusWarning, StatusOver };

Status GetStatus(int count);
int GetStatusIndicatorPin(Status status);

void setup()
{
  pinMode(startButtonPin, INPUT_PULLUP);

  disp.setDigitPins (digitPinsCount , digitPins);
  disp.setCommonCathode();
  disp.setDutyCycle(20);
  disp.setTimer(2);
  disp.startTimer();
}

void loop()
{
  if (digitalRead(startButtonPin) == LOW)
  {
    if (!isCounting)
    {
      canceled = false;      
      isCounting = true;
      counter = 0;
    }
    else
    {
      canceled = true;
      isCounting = false;
      delay(2000);
    }
  }
    
  if (isCounting)
  {    
    if (canceled)
    {
      isCounting = false;
      counter = 0;
    }
    else if (counter < maxSeconds)
    {
      // Show status indicator, and wait for one second.
      int indicatorPin = GetStatusIndicatorPin(GetStatus(counter));      
      delay(oneSecondInMS);

      // If two-digit seconds, use it as is. Otherwise prefix with "0".
      
      int seconds = counter % oneMinuteInS;
      disp.write((seconds >= 10) ? String(seconds) : "0" + String(seconds));
      counter++;
    }
    else
    {
      isCounting = false;
    }
  }
}

Status GetStatus(int count)
{
  return count < oneMinuteInS ? 
    StatusOK : 
    count < oneMinuteInS * 2 ? 
      StatusWarning : 
      StatusOver;
}

int GetStatusIndicatorPin(Status status)
{
  return status == StatusOK ? 
    statusOKIndicatorPin :
    status == StatusWarning ? 
      statusWarningIndicatorPin : 
      statusOverIndicatorPin;
}

ISR( TIMER2_COMPA_vect ) 
{
  disp.interruptAction ();
}


