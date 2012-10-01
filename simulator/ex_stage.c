#define STAGE EX

#include "ex_stage.h"
#include "pipeline.h"
#include "state_sync.h"

#include "simulator.h"

#include "cpu/cpu.h"
#include "cpu/misc.h"

static void execute(struct op *o, uint32_t inst) {
	if (o->is16)
		o->fn.fn16(inst);
	else
		o->fn.fn32(inst);
}

void tick_ex(void) {
	DBG2("start\n");

	assert(NULL != id_ex_o);
	assert(NULL != id_ex_o->name);

	// Execute
	if (in_ITblock()) {
		if (eval_cond(CORE_cpsr_read(), (read_itstate() & 0xf0) >> 4)) {
			if (printcycles) {
				printf("    P: %08d - 0x%08x : %04x (%s)\t%s\n",
						cycle, id_ex_PC - 4, id_ex_inst, id_ex_o->name,
						"ITSTATE {executed}");
			}
			execute(id_ex_o, id_ex_inst);
		} else {
			if (printcycles) {
				printf("    P: %08d - 0x%08x : %04x (%s)\t%s\n",
						cycle, id_ex_PC - 4, id_ex_inst, id_ex_o->name,
						"ITSTATE {skipped}");
			}
			//WARN("itstate skipped instruction\n");
		}
		IT_advance();
	} else {
		if (printcycles) {
			if ((id_ex_PC == STALL_PC) && (id_ex_inst == INST_NOP)) {
				printf("    P: %08d - 0x%08x : <stall>\n",
						cycle, id_ex_PC - 4);
			} else {
				printf("    P: %08d - 0x%08x : %04x (%s)\n",
						cycle, id_ex_PC - 4,
						id_ex_inst, id_ex_o->name);
			}
		}
		execute(id_ex_o, id_ex_inst);
	}

	DBG2("end\n");
}
