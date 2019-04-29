import { EventType, DataHudComunication } from "app/classes/types";



export class HudComunicationEvent {
    constructor(
        readonly type: EventType,
        readonly info: DataHudComunication = null) {
    }
}
