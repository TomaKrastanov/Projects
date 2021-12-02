#define BAUDRATE (((F_CPU / (16UL * 9600))) - 1));

void setup()
{
  UBRR0H = (BAUDRATE >> 8);
  UBRR0L = BAUDRATE;
  UCSR0A &= ~_BV(U2X0);
  UCSR0B |= _BV(RXCIE0);
  UCSR0B |= _BV(TXEN0);
  UCSR0C |= _BV(UCSZ01) | _BV(UCSZ00);
}

void loop()
{
  while(!(UCSR0A & _BV(UDRE0)));
  UDR0 = 'A';
}
