#include <Arduino.h>

void setup() {
    Serial.begin(9600);

    pinMode(A0, INPUT );
}

void loop() {
  // int value_slide_pot_a = analogRead(A0);
  Serial.println("A0:10:true");
  delay(2000);
  // Serial.println(value_slide_pot_a);
}
