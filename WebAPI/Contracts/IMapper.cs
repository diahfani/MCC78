namespace WebAPI.Contracts;

public interface IMapper<TModel, TViewModel>
{
    TModel Map(TViewModel viewModel);
    TViewModel Map(TModel model);
}
