export interface CreateReservaDto {
  nombreCliente: string;
  correoCliente: string;
  fechaHora: string;       // p.ej. '2025-07-18T10:33:17.702Z'
  cantidadPersonas: number;
  mesaId: number;
}
