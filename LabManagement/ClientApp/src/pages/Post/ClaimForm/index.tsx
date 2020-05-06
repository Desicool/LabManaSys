import { CloseCircleOutlined } from '@ant-design/icons';
import { Button, Card, Col, DatePicker, Form, Input, Popover, Row, Table } from 'antd';
import React, { FC, useState, useEffect } from 'react';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect, Dispatch, UserModelState, IUser, IPostClaimFormParam, ChemicalListModelState } from 'umi';
import styles from './style.less';
import { IChemical } from '@/models/entity';
import { TableRowSelection } from 'antd/lib/table/interface';
import moment, { Moment } from 'moment';

type InternalNamePath = (string | number)[];
const fieldLabels = {
  username: '申请人姓名',
  labid: '所属实验室编号',
  rtime: '预计归还时间',
};


interface ClaimFormProps {
  dispatch: Dispatch;
  submitting: boolean;
  currentUser?: IUser;
  chemicals: IChemical[];
}

interface ErrorField {
  name: InternalNamePath;
  errors: string[];
}

const ClaimForm: FC<ClaimFormProps> = ({
  submitting,
  dispatch,
  currentUser,
  chemicals
}) => {
  useEffect(() => {
    dispatch({
      type: 'chemicalList/fetch',
    });
  }, [1]);
  const [form] = Form.useForm();
  const [error, setError] = useState<ErrorField[]>([]);
  const [selectedRowKeys, setSelectedRowKeys] = useState<any[]>([]);
  const [selectedChemicals, setSelectedChemicals] = useState<IChemical[]>([]);
  const columns = [
    {
      title: '化学品名称',
      dataIndex: 'name',
    },
    {
      title: '化学品数量',
      dataIndex: 'amount'
    },
    {
      title: '生产厂家',
      dataIndex: 'fname',
    }
  ]
  const dataSource = chemicals.filter(u=> u.state === 'Lab').map(u => ({
    ...u,
    key: u.id
  }));
  const getErrorInfo = (errors: ErrorField[]) => {
    const errorCount = errors.filter((item) => item.errors.length > 0).length;
    if (!errors || errorCount === 0) {
      return null;
    }
    const scrollToField = (fieldKey: string) => {
      const labelNode = document.querySelector(`label[for="${fieldKey}"]`) || document.getElementById(fieldKey);
      if (labelNode) {
        labelNode.scrollIntoView(true);
      }
    };
    const errorList = errors.map((err) => {
      if (!err || err.errors.length === 0) {
        return null;
      }
      const key = err.name[0] as string;
      return (
        <li key={key} className={styles.errorListItem} onClick={() => scrollToField(key)}>
          <CloseCircleOutlined className={styles.errorIcon} />
          <div className={styles.errorMessage}>{err.errors[0]}</div>
          <div className={styles.errorField}>{fieldLabels[key]}</div>
        </li>
      );
    });
    return (
      <span className={styles.errorIcon}>
        <Popover
          title="表单校验信息"
          content={errorList}
          overlayClassName={styles.errorPopover}
          trigger="click"
          getPopupContainer={(trigger: HTMLElement) => {
            if (trigger && trigger.parentNode) {
              return trigger.parentNode as HTMLElement;
            }
            return trigger;
          }}
        >
          <CloseCircleOutlined />
        </Popover>
        {errorCount}
      </span>
    );
  };

  const onFinish = (values: { [key: string]: any }) => {
    if (selectedChemicals.length < 1) {
      setError([{ name: ['chemicals'], errors: ['需要至少选择一项化学危险品才能进行提交'] }]);
      return;
    }
    var tmp = (values.rtime as Moment).hour(23).minute(59).second(59);
    setError([]);
    const data: IPostClaimFormParam = {
      form: {
        ...values,
        rtime: tmp.toISOString(),
        uid: currentUser?.userId,
        lid: currentUser?.labId,
        uname: currentUser?.userName
      },
      chemicals: selectedChemicals
    }
    dispatch({
      type: 'postClaimForm/submitAdvancedForm',
      payload: data,
    });
  };

  const onFinishFailed = (errorInfo: any) => {
    if (selectedChemicals.length < 1) {
      errorInfo.errorFields.push({ name: ['chemicals'], errors: ['需要至少选择一项化学危险品才能进行提交'] });
    }
    console.log('Failed:', errorInfo);
    setError(errorInfo.errorFields);
  };

  const rowSelection: TableRowSelection<IChemical> = {
    selectedRowKeys,
    onChange: (selectedRowKeys, selectedRows) => {
      setSelectedRowKeys(selectedRowKeys);
      setSelectedChemicals(selectedRows);
    }
  }
  return (
    <Form
      form={form}
      layout="vertical"
      hideRequiredMark
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
    >
      <PageHeaderWrapper content="请选择你要申报领用的化学危险品并填写预计归还时间。">
        <Card title="危险品领用申报表" className={styles.card} bordered={false}>
          <Row gutter={16}>
            <Col lg={6} md={12} sm={24}>
              <Form.Item
                label={fieldLabels.username}
                name="uname"
              >
                <Input disabled={true} placeholder={currentUser?.userName}  />
              </Form.Item>
            </Col>
            <Col xl={{ span: 6, offset: 2 }} lg={{ span: 8 }} md={{ span: 12 }} sm={24}>
              <Form.Item
                label={fieldLabels.labid}
                name="lid"
              >
                <Input
                  style={{ width: '100%' }}
                  disabled={true}
                  placeholder={currentUser?.labId?.toString()}
                />
              </Form.Item>
            </Col>
            <Col xl={{ span: 8, offset: 2 }} lg={{ span: 10 }} md={{ span: 24 }} sm={24}>
              <Form.Item
                label={fieldLabels.rtime}
                name="rtime"
                rules={[{ required: true }]}
                help={'以当日23时59分59秒为截止点'}
              >
                <DatePicker disabledDate={(d) => d.diff(moment(new Date()),'days') < 0}>
                </DatePicker>
              </Form.Item>
            </Col>
          </Row>
        </Card>
        <Card title="选择需要申请的化学危险品" className={styles.card} bordered={false} id={'chemicals'}>
          <Table dataSource={dataSource} columns={columns} rowSelection={rowSelection}>
          </Table>
        </Card>
      </PageHeaderWrapper>
      <Form.Item wrapperCol={{ span: 8, offset: 8 }} style={{ marginTop: 32 }}>
        <Button type="primary" onClick={() => form.submit()} loading={submitting} style={{ width: '90%', alignSelf: 'right' }}>
          提交申请
        </Button>
        {getErrorInfo(error)}
      </Form.Item>
    </Form>
  );
};

export default connect(({
  loading,
  user,
  chemicalList,
}: {
  user: UserModelState,
  chemicalList: ChemicalListModelState,
  loading: {
    effects: { [key: string]: boolean }
  }
}) => ({
  currentUser: user.currentUser,
  chemicals: chemicalList.list,
  submitting: loading.effects['postClaimForm/submitAdvancedForm'],
}))(ClaimForm);
