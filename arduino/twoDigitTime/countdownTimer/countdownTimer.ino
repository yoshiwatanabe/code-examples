#include "SevenSeg.h"

//
// Constants
//
const int MAX_SECONDS = 180;
const int ONE_MINUTE_IN_SECONDS = 60;
const int ONE_SECOND_IN_MS = 1000;
const int CONTROL_BUTTON_PIN = 1;
const int NORMAL_STATE_LED_PIN = 4;
const int ABNORMAL_STATE_LED_PIN = 3;
const int DANGER_STATE_LED_PIN = 2;
const int DIGITS_PINS_COUNT = 2;

//
// Variables
//
int digitPins [DIGITS_PINS_COUNT] = {11, 7};
SevenSeg disp (6, 5, 10, 12, 13, 8, 9); // (A, B, C, D, E, F, G)
int counter = 0;
boolean isCounting = false;

//
// Status enum
//
enum Status { StatusNormal, StatusAbnormal, StatusDanger };

//
// Determines a Status value from a specified count value.
//
Status GetStatus(int count)
{  
  return count < ONE_MINUTE_IN_SECONDS + 1 ? 
    StatusNormal : 
    count < ONE_MINUTE_IN_SECONDS * 2 + 1 ? 
      StatusAbnormal : 
      StatusDanger;
}

//
// Determines a status indicator pin from a specified Status value.
//
int GetStatusIndicatorPin(Status status)
{
  return status == StatusNormal ? 
    NORMAL_STATE_LED_PIN :
    status == StatusAbnormal ? 
      ABNORMAL_STATE_LED_PIN : 
      DANGER_STATE_LED_PIN;
}

//
// Converts a specified number to a string representation.
//
String ToDigitString(int number)
{  
  int seconds = number % ONE_MINUTE_IN_SECONDS;
  // The following takes case of a single digit case where we
  // prefix with "0" character so it would look like "04", "05", etc.
  return (seconds >= 10) ? String(seconds) : "0" + String(seconds);
}

//
// Set all indicator LEDs to a specified value (LOW or HIGH)
//
void SetAllIndicatorsTo(int value)
{
  digitalWrite(NORMAL_STATE_LED_PIN, value);
  digitalWrite(ABNORMAL_STATE_LED_PIN, value);
  digitalWrite(DANGER_STATE_LED_PIN, value);
}

//
// Shows an animation using LEDs. This gives a clear feedback to the user.
// This animation is only for presentation purpose. Customize as you like.
//
void ShowStartingAnimation()
{
  for (int i = 0; i <12; i++)
  {
    SetAllIndicatorsTo(LOW);
    int choice = i % 3;
    switch (choice)
    {
      case 0:
      digitalWrite(NORMAL_STATE_LED_PIN, HIGH);
      break;

      case 1:
      digitalWrite(ABNORMAL_STATE_LED_PIN, HIGH);
      break;

      case 2:
      default:
      digitalWrite(DANGER_STATE_LED_PIN, HIGH);
      break;
    }

    delay(50);
    SetAllIndicatorsTo(LOW);
  }
}

//
// Setup Arduino device
//
void setup()
{
  pinMode(CONTROL_BUTTON_PIN, INPUT_PULLUP);
  pinMode(NORMAL_STATE_LED_PIN, OUTPUT);
  pinMode(ABNORMAL_STATE_LED_PIN, OUTPUT);
  pinMode(DANGER_STATE_LED_PIN, OUTPUT);

  disp.setDigitPins (DIGITS_PINS_COUNT , digitPins);
  disp.setCommonCathode();
  disp.setTimer(2);
  disp.startTimer();

  disp.write(ToDigitString(counter));
}

//
// Processing loop of Arduino device
//
void loop()
{  
  SetAllIndicatorsTo(HIGH);

  if (digitalRead(CONTROL_BUTTON_PIN) == LOW) // When pressed, it is LOW.
  {
    delay(10); // Helps to reduce missing input signal.

    if (!isCounting)
    {
      isCounting = true;
      counter = 1;
      ShowStartingAnimation();
    }
    else
    {
      isCounting = false;
      counter = 0;
      disp.write(ToDigitString(counter));
      delay(1000);      
    }
  }
    
  if (isCounting)
  { 
    if (counter < MAX_SECONDS)
    {
      // Turn all LED indicators off.
      SetAllIndicatorsTo(LOW);
      
      // Pick the right LED indicator, and light it up.
      digitalWrite(
        GetStatusIndicatorPin(
          GetStatus(counter)), 
        HIGH);

      // Block the thread of execution for about 0.5 seconds. 
      // Where is the other 0.1 second? It is used while polling Control button.
      delay(ONE_SECOND_IN_MS - 10);

      // Show the counter value in 7-segment LEDs
      disp.write(ToDigitString(counter));

      counter++;
    }
    else
    {
      isCounting = false;
      counter = 0;
      disp.write(ToDigitString(counter));
    }
  }
}

ISR( TIMER2_COMPA_vect ) 
{
  disp.interruptAction ();
}