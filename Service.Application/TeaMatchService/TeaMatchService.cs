using Business.Data.Models;
using Service.Application.Interfaces;
using Service.Application.TeaMatchService.Dto;

namespace Service.Application.TeaMatchService
{
    public class TeaMatchService
    {
        private readonly ITeaRepository _teaRepository;
        private readonly IUserRepository _userRepository;
        public TeaMatchService(ITeaRepository teaRepository, IUserRepository userRepository)
        {
            _teaRepository = teaRepository;
            _userRepository = userRepository;
        }

        public async Task<List<MatchingTeaDto>> GetMatchingTeaAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var result = new List<MatchingTeaDto>();

            var teasWithIngredients = await _teaRepository.GetWithIngredientsAsync(cancellationToken);

            var userWithIngredients = await _userRepository.GetWithIngredientsAsync(userId, cancellationToken) 
                ?? throw new ArgumentException("User does not exist");

            var userIngredients = userWithIngredients.
                                    UserIngredients
                                    .Select(ui => ui.IngredientId)
                                    .ToHashSet();
            
            foreach (var tea in teasWithIngredients) {

                if (tea.TeaIngredients.Count == 0) continue;

                decimal count = 0;

                foreach (var teaIngredient in tea.TeaIngredients)
                {
                    if (userIngredients.Contains(teaIngredient.IngredientId)) count++;
                }

                result.Add(new MatchingTeaDto
                {
                   TeaId = tea.Guid,
                   Name = tea.Name,
                   Image = tea.Image,
                   Description = tea.Description,
                   MatchPercent = count / tea.TeaIngredients.Count * 100,
                });
            }

            return [.. result.OrderByDescending(i => i.MatchPercent)];
        }
    }
}
