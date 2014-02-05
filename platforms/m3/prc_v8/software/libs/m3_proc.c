#include "m3_proc.h"

////////////////////////
// MBUS RELATED COMMANDS
////////////////////////

int write_mbus_message(uint32_t addr, uint32_t data) {
  uint32_t _mbus_addr = 0xa0000000;
  *((volatile uint32_t *) _mbus_addr) = addr;
  _mbus_addr |= 0x4;
  *((volatile uint32_t *) _mbus_addr) = data;
  return 0;
}

int enumerate(uint32_t addr) {
  uint32_t _mbus_data = 0x20000000;
  _mbus_data |= (addr << 24);
  write_mbus_message(0x0, _mbus_data);
  return 0;
}

int sleep(){
  write_mbus_message(0x1,0x0);
  return 0;
}

int write_mbus_register(uint32_t enum_addr, uint8_t reg, uint32_t data){
  uint32_t _mbus_addr = 0;
  uint32_t _mbus_data = 0;
  _mbus_addr |= (enum_addr << 4);
  _mbus_data |= (reg << 24) | data;
  write_mbus_message(_mbus_addr,_mbus_data);
  return 0;
}

int read_mbus_register(uint32_t enum_addr, uint8_t reg, uint8_t return_addr){
  uint32_t _mbus_addr = 1;
  uint32_t _mbus_data = (return_addr << 8);
  _mbus_addr |= (enum_addr << 4);
  _mbus_data |= (reg << 24);
  write_mbus_message(_mbus_addr,_mbus_data);
  return 0;
}

int read_mbus_register_RADv4(uint32_t enum_addr, uint8_t reg, uint8_t return_addr){
  uint32_t _mbus_addr = 1;
  uint32_t _mbus_data = (return_addr << 16);
  _mbus_addr |= (enum_addr << 4);
  _mbus_data |= (reg << 24);
  write_mbus_message(_mbus_addr,_mbus_data);
  return 0;
}

//////////////////////////
//PROCv6 SPECIFIC COMMANDS
//////////////////////////

// [31:4] = 28'hA200000
//  [3:2] = Target register
//  [1:0] = 2'b00
int write_config_reg(uint8_t reg, uint32_t data) {
	uint32_t _addr = 0xa2000000;
	_addr |= (reg << 2);

	*((volatile uint32_t *) _addr) = data;
	return 0;
}

//////////////////////////
//PROCv8 SPECIFIC COMMANDS
//////////////////////////
void config_timer( uint8_t timer_id, uint8_t go, uint8_t roi, uint32_t init_val, uint32_t sat_val) {
	uint32_t _addr = 0xA5000000;
	_addr |= (timer_id<<4);
	// GO  = 0x0
	// SAT = 0x4
	// ROI = 0x8
	// CNT = 0xC
	*((volatile uint32_t *) (_addr | 0x0) ) = 0x0;		// stop timer first
	*((volatile uint32_t *) (_addr | 0x4) ) = sat_val;	// set up values	
	*((volatile uint32_t *) (_addr | 0x8) ) = roi;
	*((volatile uint32_t *) (_addr | 0xC) ) = init_val;
	*((volatile uint32_t *) (_addr | 0x0) ) = go;		// run
}

void set_wakeup_timer( uint16_t timestamp, uint8_t on, uint8_t reset ) {
	uint32_t regval = timestamp;
	if( on )	regval |= 0x8000;
	else		regval &= 0x7FFF;
	*((volatile uint32_t *) 0xA2000010) = regval;

	if( reset )
		*((volatile uint32_t *) 0xA2000014) = 0x01;
}

void set_clkfreq( uint8_t fastmode, uint8_t div_cm, uint8_t div_mbus, uint8_t ring ) {
	uint32_t regval = *((volatile uint32_t *) 0xA0001028 );		// Read original reg value
	regval &= 0xFF7FC0FF;
	regval |= (fastmode<<23) | (div_cm<<12) | (div_mbus<<10) | (ring<<8) ;
	*((volatile uint32_t *) 0xA2000008) = regval;			// Write updated reg value
}

void pmu_div5_ovrd( uint8_t ovrd ) {
	uint32_t regval = *((volatile uint32_t *) 0xA000102C );		// Read original reg value
	if( ovrd )
		regval |= 0x40000000;					// Set bit 30
	else
		regval &= 0xBFFFFFFF;					// Reset bit 30
	*((volatile uint32_t *) 0xA200000C) = regval;			// Write updated reg value
}

void pmu_set_force_wake( uint8_t set ) {
	uint32_t regval = *((volatile uint32_t *) 0xA0001028 );		// Read original reg value
	if( set )
		regval |= 0x00000080;					// Set bit 7
	else
		regval &= 0xFFFFFF7F;					// Reset bit 7
	*((volatile uint32_t *) 0xA2000008) = regval;			// Write updated reg value
}

void pon_reset( void ){
	*((volatile uint32_t *) 0xA3000000) = 0x2;
}

void sleep_req_by_sw( void ){
	*((volatile uint32_t *) 0xA3000000) = 0x4;
}
