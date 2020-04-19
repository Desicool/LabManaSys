import React, { FC } from 'react';
import { connect, FormToProcessState, history } from 'umi';
import { IDeclarationForm } from '@/models/entity';
import { Table } from 'antd';
import { ColumnsType } from 'antd/lib/table';

interface DeclarationProcessProps {
    declarations: IDeclarationForm[];
    loading: boolean;
}
const DeclarationProcessComponent: FC<DeclarationProcessProps> = (props) => {
    const { declarations } = props;
    const dataSource = declarations.map(u => ({ ...u, key: u.id }));

    const columns: ColumnsType<IDeclarationForm> = [
        {
            title: '编号',
            dataIndex: 'id',
            width: '4em'
        },
        {
            title: '申请人',
            dataIndex: 'uname',
            width: '8em'
        },
        {
            title: '申请理由',
            dataIndex: 'reason'
        },
        {
            title: '进行处理',
            key: 'operation',

            render: (_, record) => <a onClick={() => history.push('/process/declear/' + record.id)}>查看详情</a>,
        },
    ];
    return <div><Table dataSource={dataSource} columns={columns} />
    </div>
}

export default connect(
    ({
        myFormToProcess,
        loading,
    }: {
        myFormToProcess: FormToProcessState;
        loading: {
            effects: { [key: string]: boolean };
        };
    }) => ({
        declarations: myFormToProcess.msg?.dform || [],
        loading: loading.effects['myFormToProcess/fetchAdvanced'],
    }),
)(DeclarationProcessComponent);
