using EjercicioTecnico.Models;
using FluentValidation;

namespace EjercicioTecnico.Middleware
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(request => request.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");

            RuleFor(request => request.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");

            RuleFor(request => request.FechaNacimiento).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria");

            RuleFor(request => request.NIT).NotEmpty().WithMessage("El NIT es obligatorio");

            RuleFor(request => request.DPI).NotEmpty().WithMessage("El DPI es obligatorio");

            RuleFor(request => request.Correo).NotEmpty().WithMessage("El Correo es obligatorio");

            RuleFor(request => request.Telefono).NotEmpty().WithMessage("El Telefono es obligatorio");

        }
    }
}
