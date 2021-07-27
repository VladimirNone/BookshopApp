import React, { Component } from 'react';

export class MainPage extends Component {
  static displayName = MainPage.name;

  render () {
      return (
		  <div className="row">
			  <div className="col border">
                  Один из трех столбцов
			  </div>
              <div className="col-7 border">

                  <div className="input-group mb-3">
                      <input type="text" className="form-control" placeholder="Название продукта" />
                      <div className="input-group-append">
                          <button className="btn btn-outline-secondary" type="button">Поиск</button>
                      </div>
                  </div>

                  <div className="row card mt-3">
                      <div className="row no-gutters">
                          <div className="col-md-4">
                              <img src="/Images/no_foto.png" alt="image" width="100%" />
                          </div>
                          <div className="col-md-8">
                              <div className="card-body">
                                  <h5 className="card-title">Заголовок карточки</h5>
                                  <p className="card-text">Это более широкая карта с вспомогательным текстом ниже в качестве естественного перехода к дополнительному контенту. Этот контент немного длиннее.</p>
                                  <p className="card-text"><small className="text-muted">Последнее обновление: 3 мин. назад</small></p>
                              </div>
                          </div>
                      </div>
                  </div>

                  <div className="row card mt-3">
                      <div className="row no-gutters">
                          <div className="col-md-4" >
                              <img src="/Images/0.jpg" alt="image" className="img-fluid rounded" height="120px" />
                          </div>
                          <div className="col-md-8">
                              <div className="card-body">
                                  <h5 className="card-title">Заголовок карточки</h5>
                                  <p className="card-text">Это более широкая карта с вспомогательным текстом ниже в качестве естественного перехода к дополнительному контенту. Этот контент немного длиннее.</p>
                                  <p className="card-text"><small className="text-muted">Последнее обновление: 3 мин. назад</small></p>
                              </div>
                          </div>
                      </div>
                  </div>

              </div>

			  <div className="col border">
				  <div className="row border">one</div>
				  <div className="row border">two</div>
				</div>
          </div>
    );
  }
}
