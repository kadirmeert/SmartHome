#include "DHT.h"
#define KIRMIZI_LED 11
#define YESIL_LED 12
#define PIR 7
#define BUZZER 22
#define led 3
#define LDR A1
#define SICAKLIK 2
#define DHTTYPE DHT11 
const int in1=8;
const int in2=9;
const int e1=4;
  int fan;
int pwm;
int pin=2;
int hareket = 0;
int sicaklikdegeri = 0;
int sicaklik=0;
int okunandeger=0;
int nem=0;
int deger;
int buzzer = 0;
int Isik_siddeti=0; 
DHT dht(SICAKLIK, DHTTYPE);
void setup() {
   dht.begin();
  pinMode(7, INPUT);
pinMode(LDR, INPUT);
pinMode(SICAKLIK, INPUT);
  pinMode(led,OUTPUT);
 pinMode(5,OUTPUT);
 pinMode(in1,OUTPUT);
   pinMode(in2,OUTPUT);
   pinMode(e1,OUTPUT);
   pinMode(BUZZER, OUTPUT);
  Serial.begin(9600);
  
}

void loop() {
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  float f = dht.readTemperature(true);
  if (isnan(h) || isnan(t) || isnan(f)) {  
    return;
  }  
  float hif = dht.computeHeatIndex(f, h);
  float hic = dht.computeHeatIndex(t, h, false);
  pwm = map(deger,700,1023,255,0); 
  int hareket;
 deger = analogRead(LDR);
 Isik_siddeti = map(deger,0,1023,0,255);
  hareket=digitalRead(7);
  delay(100);
  Serial.print((int)t);
  Serial.print(",");
  Serial.print((int)h);
  Serial.print(",");
  Serial.print(deger);
  Serial.print(",");
  Serial.print(hareket); 
  Serial.println();
  
  //c# dan gelen komut okuması
   char a= Serial.read();
    if(a == 'A'){
      analogWrite(YESIL_LED,255); 
     analogWrite(KIRMIZI_LED,LOW);
      
    }
    if(a=='B'){
      analogWrite(KIRMIZI_LED,255);
      analogWrite(YESIL_LED,LOW);
      
    }   
    if(a=='C'){
    if(hareket==1){
         
     digitalWrite(BUZZER, HIGH); }   }   
    if(a=='D'){
      if(hareket==0){
    digitalWrite(7, HIGH); 
     digitalWrite(BUZZER, LOW);
}
}
    if(a=='1'){
if(deger <300){
    analogWrite(led,255);
 }}
 if(a=='2'){
if(deger <300){
    analogWrite(led,2);
 }}
 if(a=='3'){
  if(deger > 400){
   digitalWrite(led,LOW);
  }}
  if(a=='G'){
    digitalWrite(in1,HIGH);
  digitalWrite(in2,LOW);
      analogWrite(e1,255);
  }
 if(a=='H'){
  digitalWrite(in1,HIGH);
  digitalWrite(in2,LOW);
      analogWrite(e1,75);
  }
  if(a=='F'){
  digitalWrite(in1,LOW);
  digitalWrite(in2,LOW);
     
  }

}
       
      
  
