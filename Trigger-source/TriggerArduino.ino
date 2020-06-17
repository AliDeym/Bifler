/*
Copyright (C) 2017-2018 Ali Deym
This file is part of Bifler <https://github.com/alideym/bifler>.

Bifler is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Bifler is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Bifler.  If not, see <http://www.gnu.org/licenses/>.
*/

#include <LiquidCrystal.h>

unsigned int pulses;
unsigned int caliber;
unsigned int Trigger;
String Trig, readdata, tempData;
bool stop_bardasht = true;
bool calibrating = false;

// initialize the library with the numbers of the interface pins
LiquidCrystal lcd(2, 3, 4, 5, 6, 7);

void setup() {
  caliber = 2;
  // set up the LCD's number of columns and rows:
  lcd.begin(16, 2);
  attachInterrupt(digitalPinToInterrupt(20), A_RISE, RISING);
  Serial.begin(115200);
}

void loop() {

  lcd.setCursor(0, 0);
  // print the number of seconds since reset:
  lcd.print(Trigger);

  lcd.setCursor(0, 1);
  // print the number of seconds since reset:
  lcd.print(caliber);

while(Serial.available() > 0) {
  readdata = Serial.readString();
  if (readdata.startsWith("start,"))
  {
   tempData = readdata.substring(6); 
   caliber = tempData.toInt();

   if (caliber <= 0) {
    caliber = 1;
   }
   
    Trigger = 0 ;
    pulses = 0;
    Serial.println("s,1");
    stop_bardasht = false;
  }
  
  else if (readdata == "end" )
  {
    Serial.println("s,2");
    stop_bardasht = true;
  }

  else if (readdata == "h" )
  {
    Serial.println("h,1 ");
  }
  
  else if (readdata == "caliber_start") 
  {
    Serial.println("c,1");
    caliber = 0;
    pulses = 0;
    stop_bardasht = true;
    calibrating = true;
    lcd.clear();
  }
  
  else if (readdata == "caliber_stop") 
  {
    Trig = "c,0,";
    Trig = Trig + pulses;
    Serial.println(Trig);
    caliber = pulses;
    pulses = 0;
    stop_bardasht = true;
    calibrating = false;
    lcd.clear();
  }
  break;
}

   if (pulses >= caliber && !stop_bardasht){
    lcd.clear();
    Trigger ++ ;
    pulses = 0 ;
    Trig = "t,";
    Trig = Trig + Trigger;
    Serial.println(Trig);
   // lcd.setCursor(0, 0);
   // lcd.print(Trigger);
    
   }

}
void A_RISE(){
  pulses++;

  if (calibrating) {
    Trig = "c,";
    Trig = Trig + pulses;
    
    Serial.println(Trig);
  }
}

