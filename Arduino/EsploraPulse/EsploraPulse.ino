#include <Esplora.h>

const byte CH_TINKERKIT_INA = 8;
const unsigned long READ_RATE = 50;
const unsigned int BAUD_RATE = 9600;

unsigned int readChannel(byte channel) {
  digitalWrite(A0, (channel & 1) ? HIGH : LOW);
  digitalWrite(A1, (channel & 2) ? HIGH : LOW);
  digitalWrite(A2, (channel & 4) ? HIGH : LOW);
  digitalWrite(A3, (channel & 8) ? HIGH : LOW);
  return analogRead(A4);
}

void setup() {
  Serial.begin(BAUD_RATE);

}

void loop() {
  unsigned long startTime = millis();
  
  unsigned int reading = readChannel(CH_TINKERKIT_INA);
  Serial.println(reading, DEC);

  unsigned long elapsedTime = millis() - startTime;
  if (elapsedTime < READ_RATE) {
    delay(READ_RATE-elapsedTime);
  } 
}
