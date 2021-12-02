
 #define BAUD_PRESCALLER (((F_CPU / (9600 * 16))) - 1)
 
  char arrayChar[] = "Assignment 1/n";
  size_t NumberOfElements = sizeof(arrayChar)/sizeof(arrayChar[0]);
  
void setup() {
  // put your setup code here, to run once:
  UBRR0H = (BAUD_PRESCALLER >> 8);
  UBRR0L = BAUD_PRESCALLER;
  UCSR0B |= _BV(TXEN0);
  UCSR0C |= _BV(UCSZ01) | _BV(UCSZ00);
}

void loop() {
  for (int i = 0; i < NumberOfElements; i++)
  {
    while (!(UCSR0A & _BV(UDRE0)));
    UDR0 = arrayChar[i];
  }
}
