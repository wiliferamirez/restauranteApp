export interface ReservaRequest {
  nombreCliente: string;
  correoCliente: string;
  fechaHora: string;        
  cantidadPersonas: number;
  mesaId: number;
}
export interface Reserva {
  id: number;
  nombreCliente: string;
  fechaHora: string;     
  cantidadPersonas: number;
  mesaInfo: string;     
}
