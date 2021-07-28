import React, { Component } from 'react';

export class Card extends Component {

    render() {
        return (
            <div className="card mt-3">
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
            </div>);
    }
}