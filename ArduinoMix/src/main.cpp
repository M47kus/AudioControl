#include <Arduino.h>
#include <ezButton.h>
#include <string>

#define BUTTON_NUM 6 // the number of buttons

#define BUTTON_PIN_1 12 // The Arduino pin connected to the button 1
#define BUTTON_PIN_2 11 // The Arduino pin connected to the button 2
#define BUTTON_PIN_3 8  // The Arduino pin connected to the button 3
#define BUTTON_PIN_4 4  // The Arduino pin connected to the button 4
#define BUTTON_PIN_5 6  // The Arduino pin connected to the button 5
#define BUTTON_PIN_6 2  // The Arduino pin connected to the button 6

#define slide_PIN_1 A0
#define slide_PIN_2 A1
#define slide_PIN_3 A2
#define slide_PIN_4 A3
#define slide_PIN_5 A4
#define slide_PIN_6 A5

ezButton button1(BUTTON_PIN_1); // create ezButton object for button 1
ezButton button2(BUTTON_PIN_2); // create ezButton object for button 2
ezButton button3(BUTTON_PIN_3); // create ezButton object for button 3
ezButton button4(BUTTON_PIN_4); // create ezButton object for button 4
ezButton button5(BUTTON_PIN_5); // create ezButton object for button 5
ezButton button6(BUTTON_PIN_6); // create ezButton object for button 5

int slide_value_1 = 0;
int slide_value_2 = 0;
int slide_value_3 = 0;
int slide_value_4 = 0;
int slide_value_5 = 0;
int slide_value_6 = 0;

bool mute_1 = false;
bool mute_2 = false;
bool mute_3 = false;
bool mute_4 = false;
bool mute_5 = false;
bool mute_6 = false;

float slider_offset = 1;

bool inrange(int value_1, int value_2, float range)
{
  if (value_1 >= value_2 - range && value_1 <= value_2 + range)
  {
    return true;
    if(value_1 == 0 || value_1 == 100) {
      return false;
    }
  }
  else
  {
    return false;
  }
}

std::string conv_bool(bool input)
{
  // using ternary operators
  return input ? "true" : "false";
}

void setup()
{
  Serial.begin(9600);

  pinMode(slide_PIN_1, INPUT);
  pinMode(slide_PIN_2, INPUT);
  pinMode(slide_PIN_3, INPUT);
  pinMode(slide_PIN_4, INPUT);
  pinMode(slide_PIN_5, INPUT);
  pinMode(slide_PIN_6, INPUT);

  button1.setDebounceTime(100); // set debounce time to 100 milliseconds
  button2.setDebounceTime(100); // set debounce time to 100 milliseconds
  button3.setDebounceTime(100); // set debounce time to 100 milliseconds
  button4.setDebounceTime(100); // set debounce time to 100 milliseconds
  button5.setDebounceTime(100); // set debounce time to 100 milliseconds
  button6.setDebounceTime(100); // set debounce time to 100 milliseconds
}

void loop()
{
  button1.loop(); // MUST call the loop() function first
  button2.loop(); // MUST call the loop() function first
  button3.loop(); // MUST call the loop() function first
  button4.loop(); // MUST call the loop() function first
  button5.loop(); // MUST call the loop() function first
  button6.loop(); // MUST call the loop() function first

  int slide_1 = analogRead(slide_PIN_1);
  int slide_2 = analogRead(slide_PIN_2);
  int slide_3 = analogRead(slide_PIN_3);
  int slide_4 = analogRead(slide_PIN_4);
  int slide_5 = analogRead(slide_PIN_5);
  int slide_6 = analogRead(slide_PIN_6);

  int value_1 = map(slide_1, 0, 1023, 0, 100);
  int value_2 = map(slide_2, 0, 1023, 0, 100);
  int value_3 = map(slide_3, 0, 1023, 0, 100);
  int value_4 = map(slide_4, 0, 1023, 0, 100);
  int value_5 = map(slide_5, 0, 1023, 0, 100);
  int value_6 = map(slide_6, 0, 1023, 0, 100);

  // Serial.print("\n");
  // Serial.print(value_1);
  // Serial.print("\t");
  // Serial.print(value_2);
  // Serial.print("\t");
  // Serial.print(value_3);
  // Serial.print("\t");
  // Serial.print(value_4);
  // Serial.print("\t");
  // Serial.print(value_5);
  // Serial.print("\t");
  // Serial.print(value_6);

  delay(100);

  std::string str1 = "A0:" + std::to_string(value_1) + ":" + conv_bool(mute_1);
    // Serial.println(str1.c_str());
    // slide_value_1 = value_1;

  if (button1.isReleased())
  {
    if (mute_1 == true)
    {
      mute_1 = false;
    }
    else
    {
      mute_1 = true;
    }
    // std::string str = "A0:" + std::to_string(value_1) + ":" + conv_bool(mute_1);
    // Serial.println(str.c_str());
  }

  std::string str2 = "A1:" + std::to_string(value_2) + ":" + conv_bool(mute_1);
    // Serial.println(str2.c_str());
    // slide_value_2 = value_2;

  if (button2.isReleased())
  {
    if (mute_2 == true)
    {
      mute_2 = false;
    }
    else
    {
      mute_2 = true;
    }
    // std::string str = "A1:" + std::to_string(value_2) + ":" + conv_bool(mute_2);
    // Serial.println(str.c_str());
  }

  std::string str3 = "A2:" + std::to_string(value_3) + ":" + conv_bool(mute_3);
    // Serial.println(str3.c_str());
    // slide_value_3 = value_3;

  if (button3.isReleased())
  {
    if (mute_3 == true)
    {
      mute_3 = false;
    }
    else
    {
      mute_3 = true;
    }
    // std::string str = "A2:" + std::to_string(value_3) + ":" + conv_bool(mute_3);
    // Serial.println(str.c_str());
  }

  std::string str4 = "A3:" + std::to_string(value_4) + ":" + conv_bool(mute_4);
    // Serial.println(str4.c_str());
    // slide_value_4 = value_4;

  if (button4.isReleased())
  {
    if (mute_4 == true)
    {
      mute_4 = false;
    }
    else
    {
      mute_4 = true;
    }
    // std::string str = "A3:" + std::to_string(value_4) + ":" + conv_bool(mute_4);
    // Serial.println(str.c_str());
  }

  std::string str5 = "A4:" + std::to_string(value_5) + ":" + conv_bool(mute_5);
    // Serial.println(str5.c_str());
    // slide_value_5 = value_5;

  if (button5.isReleased())
  {
    if (mute_5 == true)
    {
      mute_5 = false;
    }
    else
    {
      mute_5 = true;
    }
    // std::string str = "A4:" + std::to_string(value_5) + ":" + conv_bool(mute_5);
    // Serial.println(str.c_str());
  }

  std::string str6 = "A5:" + std::to_string(value_6) + ":" + conv_bool(mute_6);
    // Serial.println(str6.c_str());
    // slide_value_6 = value_6;

  if (button6.isReleased())
  {
    if (mute_6 == true)
    {
      mute_6 = false;
    }
    else
    {
      mute_6 = true;
    }
    // std::string str = "A5:" + std::to_string(value_6) + ":" + conv_bool(mute_6);
    //Serial.println(str.c_str());
  }

  std::string send = str1 + ";" + str1 + ";" + str2 + ";" + str3 + ";" + str4 + ";" + str5 + ";" + str6 + ";";

  Serial.println(send.c_str());
}

